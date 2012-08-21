﻿//    -------------------------------------------------------------------------------------------- 
//    Copyright (c) 2011 Microsoft Corporation.  All rights reserved. 
//    Use of this sample source code is subject to the terms of the Microsoft license 
//    agreement under which you licensed this sample source code and is provided AS-IS. 
//    If you did not accept the terms of the license agreement, you are not authorized 
//    to use this sample source code.  For the terms of the license, please see the 
//    license agreement between you and Microsoft. 
﻿//    -------------------------------------------------------------------------------------------- 

 using System.Windows;

namespace ConferenceStarterKit.ViewModels
{
    public class ViewModelBase : ModelBase
    {
        public string ApplicationName
        {
            get { return Settings.ApplicationName; }            
        }

        private string _PageTitle;
        public string PageTitle
        {
            get { return _PageTitle; }
            set
            {
                if (_PageTitle == value)
                    return;
                _PageTitle = value;
                NotifyPropertyChanged("PageTitle");
            }
        }

        private bool _IsDataLoaded;
        public bool IsDataLoaded
        {
            get { return _IsDataLoaded; }
            set
            {
                if (_IsDataLoaded == value)
                    return;
                _IsDataLoaded = value;
                NotifyPropertyChanged("IsDataLoaded");
            }
        }

        private string _DataStatus;
        public string DataStatus
        {
            get { return _DataStatus; }
            set
            {
                if (_DataStatus == value)
                    return;
                _DataStatus = value;
                NotifyPropertyChanged("DataStatus");
            }
        }

        private static bool? _isInDesignMode;
        public static bool IsInDesignMode()
        {
            if (!_isInDesignMode.HasValue)
            {
                _isInDesignMode =
                    (null == Application.Current) ||
                    Application.Current.GetType() == typeof(Application);
            }
            return _isInDesignMode.Value;
        }      
    }
}
