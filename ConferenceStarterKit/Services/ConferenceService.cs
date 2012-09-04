﻿//    -------------------------------------------------------------------------------------------- 
//    Copyright (c) 2011 Microsoft Corporation.  All rights reserved. 
//    Use of this sample source code is subject to the terms of the Microsoft license 
//    agreement under which you licensed this sample source code and is provided AS-IS. 
//    If you did not accept the terms of the license agreement, you are not authorized 
//    to use this sample source code.  For the terms of the license, please see the 
//    license agreement between you and Microsoft. 
﻿//    -------------------------------------------------------------------------------------------- 

using System;
using System.IO.IsolatedStorage;
using System.Net;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Linq;
using System.Collections;
using ConferenceStarterKit.ViewModels;
using System.Collections.Generic;
using System.Data.Services.Client;


namespace ConferenceStarterKit.Services
{
    public class ConferenceService : ModelBase, IConferenceService
    {
        public ObservableCollection<SessionItemModel> SessionList { get; set; }
        public ObservableCollection<SpeakerItemModel> SpeakerList { get; set; }
        private ObservableCollection<TwitterStatusItemModel> TwitterFeed;

        public event LoadEventHandler DataLoaded;

        public ObservableCollection<SessionItemModel> GetSessions()
        {
            return SessionList;
        }

        public void GetData()
        {
            SessionList = new ObservableCollection<SessionItemModel>();
            SpeakerList = new ObservableCollection<SpeakerItemModel>();
            DateTime sessionLastDownload = DateTime.MinValue;

            // Session data
            // Get the last time the data was downloaded.
            if (IsolatedStorageSettings.ApplicationSettings.Contains("SessionLastDownload"))
            {
                sessionLastDownload = (DateTime)IsolatedStorageSettings.ApplicationSettings["SessionLastDownload"];
            }

            // Check if we need to download the latest data, or if we can just use the isolated storage data
            // Cache the data for 2 hours
            if ((sessionLastDownload.AddHours(2) >= DateTime.Now) && IsolatedStorageSettings.ApplicationSettings.Contains("SessionData"))
            {
                // Get the data from Isolated storage
                if (IsolatedStorageSettings.ApplicationSettings.Contains("SessionData"))
                {
                    var converted = (IsolatedStorageSettings.ApplicationSettings["SessionData"] as IEnumerable<SessionItemModel>);

                    SessionList = converted.ToObservableCollection(SessionList);
                    SpeakerList = SessionList.SelectMany(p => p.Speakers).Distinct().OrderBy(p => p.SurnameFirstname).ToObservableCollection(SpeakerList);
                    var loadedEventArgs = new LoadEventArgs { IsLoaded = true, Message = string.Empty };
                    OnDataLoaded(loadedEventArgs);
                }
            }
            else
            {
                // Download the data
                var techEdService = new TechEdServiceReference.ODataTEEntities(new Uri(Settings.SessionServiceUri));
                var sessionsQuery = from s in techEdService.Sessions//.Take(20)
                                    select new SessionTemp
                                               {
                                                   SessionId = s.SessionID,
                                                   Code = s.Code,
                                                   Title = s.Title,
                                                   Description = s.Abstract,
                                                   Room = s.Room,
                                                   StartTime = s.StartTime,
                                                   Speakers = s.Speakers.Select(p => new SpeakerTemp { SpeakerId = p.SpeakerID, First = p.SpeakerFirstName, Last = p.SpeakerLastName, Twitter = p.Twitter, SmallImage = p.SmallImage })
                                               };

                ((DataServiceQuery)sessionsQuery).BeginExecute(OnCustomerOrdersQueryComplete, sessionsQuery);
            }
        }

        class SessionTemp
        {
            public int SessionId { get; set; }
            public string Code { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Room { get; set; }
            public DateTime? StartTime { get; set; }
            public IEnumerable<SpeakerTemp> Speakers { get; set; }
        }
        class SpeakerTemp
        {
            public int SpeakerId { get; set; }
            public string First { get; set; }
            public string Last { get; set; }
            public string Twitter { get; set; }
            public string SmallImage { get; set; }
        }
        private void OnCustomerOrdersQueryComplete(IAsyncResult result)
        {
            try
            {
                var svcContext = result.AsyncState as DataServiceQuery;
                if (svcContext != null)
                {
                    var sessionData = svcContext.EndExecute(result);

                    var converted = (from s in ((IEnumerable<SessionTemp>)sessionData)
                                     orderby s.Code
                                     select new SessionItemModel
                                                {
                                                    Code = s.Code,
                                                    Title = s.Title,
                                                    Description = StripHtmlTags(s.Description),
                                                    Location = s.Room,
                                                    Id = s.SessionId,
                                                    Date = (DateTime)s.StartTime,
                                                    Speakers = s.Speakers.Select(p => new SpeakerItemModel { Id = p.SpeakerId, FirstName = p.First, LastName = p.Last }).ToObservableCollection()
                                                }).ToList();

                    // Update the SessionList collection. Then mark everything as loaded
                    System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        SessionList = converted.ToObservableCollection(SessionList);
                        SpeakerList = SessionList.SelectMany(p => p.Speakers).Distinct().OrderBy(p => p.SurnameFirstname).ToObservableCollection(SpeakerList);
                        var loadedEventArgs = new LoadEventArgs { IsLoaded = true, Message = string.Empty };
                        OnDataLoaded(loadedEventArgs);
                    });


                    // Save the results into the cache.
                    // First save the data
                    if (IsolatedStorageSettings.ApplicationSettings.Contains("SessionData"))
                        IsolatedStorageSettings.ApplicationSettings.Remove("SessionData");
                    IsolatedStorageSettings.ApplicationSettings.Add("SessionData", converted);

                    // then update the last updated key
                    if (IsolatedStorageSettings.ApplicationSettings.Contains("SessionLastDownload"))
                        IsolatedStorageSettings.ApplicationSettings.Remove("SessionLastDownload");
                    IsolatedStorageSettings.ApplicationSettings.Add("SessionLastDownload", DateTime.Now);
                    IsolatedStorageSettings.ApplicationSettings.Save(); // trigger a save

                }
            }
            catch (DataServiceQueryException)
            {
                System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    var loadedEventArgs = new LoadEventArgs { IsLoaded = false, Message = "There was a network error. Close the app and try again." };
                    OnDataLoaded(loadedEventArgs);
                    System.Windows.MessageBox.Show("There was a network error. Close the app and try again.");
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        ConferenceResponse response;
        void SessionCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            LoadEventArgs loadedEventArgs = new LoadEventArgs();

            try
            {
                response = new ConferenceResponse(e.Result);

                IEnumerable<JsonTypes.Speaker> speakers = response.Speakers.ToList();
                IEnumerable<JsonTypes.Session> sessions = response.Sessions.ToList();
                IEnumerable<JsonTypes.TimeSlot> timeslots = response.TimeSlots.ToList();

                SpeakerList = (from s in speakers
                               select new SpeakerItemModel
                               {
                                   Bio = StripHtmlTags(s.Bio),
                                   Id = s.Id,
                                   FirstName = s.FirstName,
                                   LastName = s.LastName,
                                   Position = s.Position,
                                   PictureUrl = string.Format("http://phillyemergingtech.com/2011{0}", s.PictureUrl),
                                   Twitter = s.Twitter

                               }).ToObservableCollection(SpeakerList);

                SessionList = (from s in sessions
                               select new SessionItemModel
                               {
                                   Title = s.Name,
                                   Date = DateTime.Parse((from t in timeslots
                                                          where t.Id == s.TimeSlotId
                                                          select t.StartTime).First()),
                                   Description = StripHtmlTags(s.Description),
                                   Location = s.SessionLocationName,
                                   Id = s.Id,
                                   Speakers = (from spk in SpeakerList
                                               where s.SpeakerIds.Contains(spk.Id)
                                               select spk).ToObservableCollection()
                               }).ToObservableCollection(SessionList);

                loadedEventArgs.IsLoaded = true;
                loadedEventArgs.Message = string.Empty;
            }
            catch (Exception ex)
            {
                loadedEventArgs.IsLoaded = false;
                loadedEventArgs.Message = "unable to load data";
            }
            finally
            {
                OnDataLoaded(loadedEventArgs);
            }


        }

        protected virtual void OnDataLoaded(LoadEventArgs e)
        {
            if (DataLoaded != null)
            {
                DataLoaded(this, e);
            }
        }

        public ObservableCollection<SpeakerItemModel> GetSpeakers()
        {
            return SpeakerList;
        }

        public ObservableCollection<TwitterStatusItemModel> GetTwitterFeed(string TwitterId)
        {
            TwitterFeed = new ObservableCollection<TwitterStatusItemModel>();
            WebClient wc = new WebClient();
            wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(TwitterStatusInformationCompleted);
            wc.DownloadStringAsync(new Uri(string.Format("{0}{1}", Settings.TwitterServiceUri, TwitterId)));
            return TwitterFeed;
        }

        void TwitterStatusInformationCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                var doc = XDocument.Parse(e.Result);
                foreach (var item in doc.Descendants("status"))
                {
                    var model = new TwitterStatusItemModel()
                    {
                        Date = item.Element("created_at").Value.Substring(0, item.Element("created_at").Value.IndexOf('+')),
                        Text = item.Element("text").Value,
                    };
                    TwitterFeed.Add(model);
                }

            }
            catch (Exception)
            {

            }


            TwitterFeed = null;
        }

        private string StripHtmlTags(string value)
        {
            const string HTML_TAG_PATTERN = "<.*?>";
            var result = Regex.Replace(value, HTML_TAG_PATTERN, string.Empty);
            return result;
        }
    }
}
