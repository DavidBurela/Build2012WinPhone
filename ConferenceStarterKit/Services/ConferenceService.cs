﻿//    -------------------------------------------------------------------------------------------- 
//    Copyright (c) 2011 Microsoft Corporation.  All rights reserved. 
//    Use of this sample source code is subject to the terms of the Microsoft license 
//    agreement under which you licensed this sample source code and is provided AS-IS. 
//    If you did not accept the terms of the license agreement, you are not authorized 
//    to use this sample source code.  For the terms of the license, please see the 
//    license agreement between you and Microsoft. 
﻿//    -------------------------------------------------------------------------------------------- 

using System;
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

            var techEdService = new TechEdServiceReference.ODataTEEntities(new Uri(@"http://odata.msteched.com/teau12/sessions.svc/"));
            var sessionsQuery = from s in techEdService.Sessions
                                select s;

            ((DataServiceQuery<TechEdServiceReference.Session>)sessionsQuery).BeginExecute(OnCustomerOrdersQueryComplete, sessionsQuery);        
        }

        private void OnCustomerOrdersQueryComplete(IAsyncResult result)
        {
            try
            {
                var svcContext = result.AsyncState as DataServiceQuery<TechEdServiceReference.Session>;
                var response = svcContext.EndExecute(result);

                var converted = from s in response
                                select new SessionItemModel
                                {
                                    Title = s.Title,
                                    //Date = DateTime.Parse((from t in timeslots
                                    //                       where t.Id == s.TimeSlotId
                                    //                       select t.StartTime).First()),
                                    Description = StripHtmlTags(s.Abstract),
                                    //Location = s.SessionLocationName,
                                    Id = s.SessionID,
                                    Date = (DateTime)s.StartTime,
                                    //Speakers = (from spk in SpeakerList
                                    //            where s.SpeakerIds.Contains(spk.Id)
                                    //            select spk).ToObservableCollection()
                                };


                System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() =>
                                                                             {
                                                                                 SessionList = converted.ToObservableCollection(SessionList);
                                                                                 LoadEventArgs loadedEventArgs = new LoadEventArgs();
                                                                                 loadedEventArgs.IsLoaded = true;
                                                                                 loadedEventArgs.Message = string.Empty;
                                                                                 OnDataLoaded(loadedEventArgs);
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
