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
    public class SpeakerItemModel : ModelBase
    {
        private int _id;
        private string _firstname;
        private string _lastname;
        private string _bio;
        private string _position;
        private string _pictureurl;
        private string _twitter;

        public ObservableCollection<SessionItemModel> Sessions { get; private set; }

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
        public string FirstName
        {
            get { return _firstname; }
            set
            {
                if (_firstname == value)
                    return;
                _firstname = value;
                NotifyPropertyChanged("FirstName");
            }
        }
        public string LastName
        {
            get { return _lastname; }
            set
            {
                if (_lastname == value)
                    return;
                _lastname = value;
                NotifyPropertyChanged("LastName");
            }
        }

        public string SurnameFirstname { get { return string.Concat(LastName, ", ", FirstName); }}
        public string Bio
        {
            get { return _bio; }
            set
            {
                if (_bio == value)
                    return;
                _bio = value;
                NotifyPropertyChanged("Bio");
            }
        }
        public string Position
        {
            get
            {
                return _position;
            }
            set
            {
                if (value != _position)
                {
                    _position = value;
                    NotifyPropertyChanged("Position");
                }
            }
        }
        public string PictureUrl
        {
            get
            {
                return _pictureurl;
            }
            set
            {
                if (value != _pictureurl)
                {
                    _pictureurl = value;
                    NotifyPropertyChanged("PictureUrl");
                }
            }
        }
        public string Twitter
        {
            get
            {
                return _twitter;
            }
            set
            {
                if (value != _twitter)
                {
                    _twitter = value;
                    NotifyPropertyChanged("Twitter");
                }
            }
        }       
   }
}