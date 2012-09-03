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

namespace ConferenceStarterKit.ViewModels
{
    public class SessionItemModel : ModelBase
    {
        private int _id;
        private string _code;
        private string _title;
        private DateTime _date;
        private string _description;
        private string _sponsor;
        private string _location;        
        private string _track;
        private ObservableCollection<SpeakerItemModel> _speakers;

        //these are available on the json service. Not sure how to use them
        private int _timeslotid;
        private int _primarytagid;
        private int[] _tagids;

        public int Id
        {
            get { return _id; }
            set
            {
                if (_id == value)
                    return;
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }
        public string Code
        {
            get { return _code; }
            set
            {
                if (_code == value)
                    return;
                _code = value;
                NotifyPropertyChanged("Code");
            }
        }
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title == value)
                    return;
                _title = value;
                NotifyPropertyChanged("Title");
            }
        }
        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                if (value != _date)
                {
                    _date = value;
                    NotifyPropertyChanged("Date");
                }
            }
        }
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (value != _description)
                {
                    _description = value;
                    NotifyPropertyChanged("Description");
                }
            }
        }
        public string Sponsor
        {
            get
            {
                return _sponsor;
            }
            set
            {
                if (value != _sponsor)
                {
                    _sponsor = value;
                    NotifyPropertyChanged("Sponsor");
                }
            }
        }
        public string Location
        {
            get
            {
                return _location;
            }
            set
            {
                if (value != _location)
                {
                    _location = value;
                    NotifyPropertyChanged("Location");
                }
            }
        }
        public string Track
        {
            get { return _track; }
            set
            {
                if (_track == value)
                    return;
                _track = value;
                NotifyPropertyChanged("Track");
            }
        }

        public string CodeAndTitle { get { return String.Concat(Code, " - ", Title); }}
        public ObservableCollection<SpeakerItemModel> Speakers{get;set;}
            
        }
     
}