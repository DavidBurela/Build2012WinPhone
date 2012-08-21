﻿//    -------------------------------------------------------------------------------------------- 
//    Copyright (c) 2011 Microsoft Corporation.  All rights reserved. 
//    Use of this sample source code is subject to the terms of the Microsoft license 
//    agreement under which you licensed this sample source code and is provided AS-IS. 
//    If you did not accept the terms of the license agreement, you are not authorized 
//    to use this sample source code.  For the terms of the license, please see the 
//    license agreement between you and Microsoft. 
﻿//    -------------------------------------------------------------------------------------------- 
 
using System;
using System.Collections.ObjectModel;
using ConferenceStarterKit.ViewModels;

namespace ConferenceStarterKit.Services
{
    public class ConferenceServiceMock : IConferenceService
    {
        public event LoadEventHandler DataLoaded;

        public void GetData()
        {
            
        }


        public ObservableCollection<SessionItemModel> GetSessions()
        {
            ObservableCollection<SessionItemModel> Sessions = new ObservableCollection<SessionItemModel>();

            Sessions.Add(new SessionItemModel { Date = DateTime.Now, Description = "Some session information that I am sure you would like", Title = "Introduction To WP" });

            return Sessions;
        }

        public ObservableCollection<TwitterStatusItemModel> GetTwitterFeed(string TwitterId)
        {
            return null;
        }

        public ObservableCollection<SpeakerItemModel> GetSpeakers()
        {
            ObservableCollection<SpeakerItemModel> speakers = new ObservableCollection<SpeakerItemModel>();

            speakers.Add(new SpeakerItemModel{ Twitter="@DaniDiaz", Position="DE", PictureUrl="sds", LastName="Diaz", FirstName="Danilo"});
            return speakers;
        }
    }
}
