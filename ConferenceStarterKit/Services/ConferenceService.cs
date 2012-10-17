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


            // Get the data from Isolated storage if it is there
            if (IsolatedStorageSettings.ApplicationSettings.Contains("SessionData"))
            {
                var converted = (IsolatedStorageSettings.ApplicationSettings["SessionData"] as IEnumerable<SessionItemModel>);

                SessionList = converted.ToObservableCollection(SessionList);
                var loadedEventArgs = new LoadEventArgs { IsLoaded = true, Message = string.Empty };
                OnDataLoaded(loadedEventArgs);
            }
            // Get the data from Isolated storage if it is there
            if (IsolatedStorageSettings.ApplicationSettings.Contains("SpeakerData"))
            {
                var converted = (IsolatedStorageSettings.ApplicationSettings["SpeakerData"] as IEnumerable<SpeakerItemModel>);

                SpeakerList = converted.ToObservableCollection(SpeakerList);
                var loadedEventArgs = new LoadEventArgs { IsLoaded = true, Message = string.Empty };
                OnDataLoaded(loadedEventArgs);
            }


            // Get the last time the data was downloaded.
            if (IsolatedStorageSettings.ApplicationSettings.Contains("SessionLastDownload"))
            {
                sessionLastDownload = (DateTime)IsolatedStorageSettings.ApplicationSettings["SessionLastDownload"];
            }

            // Check if we need to download the latest data, or if we can just use the isolated storage data
            // Cache the data for 2 hours
            if ((sessionLastDownload.AddHours(2) < DateTime.Now) || !IsolatedStorageSettings.ApplicationSettings.Contains("SessionData"))
            {
                // Download the session data
                var sessionWebClient = new SharpGIS.GZipWebClient();
                sessionWebClient.DownloadStringCompleted += sessionWebClient_DownloadStringCompleted;
                sessionWebClient.DownloadStringAsync(new Uri(Settings.SessionServiceUri));

                // Download speaker data
                var speakerWebClient = new SharpGIS.GZipWebClient();
                speakerWebClient.DownloadStringCompleted += speakerWebClient_DownloadStringCompleted;
                speakerWebClient.DownloadStringAsync(new Uri(Settings.SpeakerServiceUri));
            }
        }

        public class SessionsResponse
        {
            public string Id { get; set; }
            public string Starts { get; set; }
            public string Finish { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public List<string> SpeakerIds { get; set; }
            public string Link { get; set; }
            public string Slides { get; set; }
            public List<string> Tags { get; set; }
            public string Code { get; set; }
            public object Level { get; set; }
            public string MediaDuration { get; set; }
            public bool IsKeyNote { get; set; }
            public bool Prerecorded { get; set; }
            public bool Notrecorded { get; set; }
            public string Previewimage { get; set; }
            public string ThumbnailImage { get; set; }
            public string Video { get; set; }
            public string Mp4med { get; set; }
            public string Mp4high { get; set; }
            public string Mp4low { get; set; }
            public string Wmvhq { get; set; }
            public string Wmv { get; set; }
            public string Smooth { get; set; }
            public string Room { get; set; }
        }

        public class SpeakerResponse
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string First { get; set; }
            public string Last { get; set; }
            public string Bio { get; set; }
            public string Photo { get; set; }
            public List<string> SessionIds { get; set; }
        }

        void sessionWebClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                var deserialized = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SessionsResponse>>(e.Result);
                {
                    DateTime tempTime;
                    var converted = (from s in deserialized
                                     orderby s.Code
                                     select new SessionItemModel
                                                {
                                                    Id = s.Id,
                                                    Code = s.Code,
                                                    Title = s.Title,
                                                    Description = StripHtmlTags(s.Description),
                                                    Location = s.Room,
                                                    Date = (DateTime.TryParse(s.Starts, out tempTime) ? DateTime.Parse(s.Starts) : DateTime.MinValue),
                                                    //Speakers = s.Speakers.Select(p => new SpeakerItemModel { Id = p.SpeakerId, FirstName = p.First, LastName = p.Last, PictureUrl = p.SmallImage }).ToObservableCollection()

                                                    //Build specific
                                                    SpeakerIds = (s.SpeakerIds != null ? s.SpeakerIds.ToObservableCollection() : null),
                                                    Link = s.Link,
                                                    Thumbnail = s.ThumbnailImage
                                                }).ToList();

                    // Display the data on the screen ONLY if we didn't already load from the cache
                    // Don't bother about rebinding everything, just wait until the user launches the next time.
                    if (SessionList.Count < 1)
                        System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() =>
                        {
                            SessionList = converted.ToObservableCollection(SessionList);
                            //SpeakerList = SessionList.SelectMany(p => p.Speakers).Distinct().OrderBy(p => p.SurnameFirstname).ToObservableCollection(SpeakerList);
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
            catch (WebException)
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

        void speakerWebClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                var deserialized = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SpeakerResponse>>(e.Result);
                {
                    var converted = (from s in deserialized
                                     orderby s.Last
                                     select new SpeakerItemModel
                                     {
                                         Id = s.Id,
                                         Bio = StripHtmlTags(s.Bio ?? ""),
                                         FirstName = s.First,
                                         LastName = s.Last,
                                         PictureUrl = s.Photo,

                                         //Build specific
                                         SessionIds = (s.SessionIds != null ? s.SessionIds.ToObservableCollection() : null),
                                         
                                     }).ToList();

                    // Display the data on the screen ONLY if we didn't already load from the cache
                    // Don't bother about rebinding everything, just wait until the user launches the next time.
                    if (SpeakerList.Count < 1)
                        System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() =>
                        {
                            SpeakerList = converted.ToObservableCollection(SpeakerList);
                            var loadedEventArgs = new LoadEventArgs { IsLoaded = true, Message = string.Empty };
                            OnDataLoaded(loadedEventArgs);
                        });


                    // Save the results into the cache.
                    // First save the data
                    if (IsolatedStorageSettings.ApplicationSettings.Contains("SpeakerData"))
                        IsolatedStorageSettings.ApplicationSettings.Remove("SpeakerData");
                    IsolatedStorageSettings.ApplicationSettings.Add("SpeakerData", converted);

                    // then update the last updated key
                    if (IsolatedStorageSettings.ApplicationSettings.Contains("SpeakerLastDownload"))
                        IsolatedStorageSettings.ApplicationSettings.Remove("SpeakerLastDownload");
                    IsolatedStorageSettings.ApplicationSettings.Add("SpeakerLastDownload", DateTime.Now);
                    IsolatedStorageSettings.ApplicationSettings.Save(); // trigger a save

                }
            }
            catch (WebException)
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
