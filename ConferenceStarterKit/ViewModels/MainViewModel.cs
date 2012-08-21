﻿//    -------------------------------------------------------------------------------------------- 
//    Copyright (c) 2011 Microsoft Corporation.  All rights reserved. 
//    Use of this sample source code is subject to the terms of the Microsoft license 
//    agreement under which you licensed this sample source code and is provided AS-IS. 
//    If you did not accept the terms of the license agreement, you are not authorized 
//    to use this sample source code.  For the terms of the license, please see the 
//    license agreement between you and Microsoft. 
﻿//    -------------------------------------------------------------------------------------------- 

using System.Collections.ObjectModel;
using System.Linq;
using ConferenceStarterKit.Services;
using System.Collections.Generic;

namespace ConferenceStarterKit.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        string _filter;
        private bool _isloading;

        public ObservableCollection<SessionItemModel> Sessions { get; set; }
        public ObservableCollection<SpeakerItemModel> Speakers { get; set; }
        public ObservableCollection<SessionItemModel> SavedSessions { get; private set; }
        public IEnumerable<Group<SessionItemModel>> SessionsByTimeSlot { get; private set; }

        public bool IsLoading
        {
            get { return _isloading; }
            set
            {
                if (_isloading == value)
                    return;
                _isloading = value;
                NotifyPropertyChanged("IsLoading");
            }
        }
        public string Filter
        {
            get { return _filter; }
            set
            {
                if (_filter == value)
                    return;
                _filter = value;
                NotifyPropertyChanged("Filter");
            }
        }
       
        public MainViewModel()
        {
            Service.DataLoaded += new LoadEventHandler(Service_DataLoaded);
            IsLoading = true;
            LoadData();           
        }

        void Service_DataLoaded(object sender, LoadEventArgs e)
        {
            this.IsDataLoaded = e.IsLoaded;
            IsLoading = false;
        }        
 

        private IConferenceService _Service;
        public IConferenceService Service
        {
            get
            {
                if (_Service == null)
                {
                    if (IsInDesignMode())
                        _Service = new ConferenceServiceMock();
                    else
                        _Service = new ConferenceService();
                }

                return _Service;
            }

            set
            {
                _Service = value;
            }
        }

        public void LoadData()
        {
            Service.GetData();

            Sessions = Service.GetSessions();
            Speakers = Service.GetSpeakers();

            //needs to get it from iso if available...
            App.Sessions = Sessions;
            App.Speakers = Speakers;

            if (App.SavedSessions == null)
                App.SavedSessions = new ObservableCollection<SessionItemModel>();

            SavedSessions = App.SavedSessions;

            
        }       
    }
}