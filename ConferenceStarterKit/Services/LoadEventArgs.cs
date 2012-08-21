﻿//    -------------------------------------------------------------------------------------------- 
//    Copyright (c) 2011 Microsoft Corporation.  All rights reserved. 
//    Use of this sample source code is subject to the terms of the Microsoft license 
//    agreement under which you licensed this sample source code and is provided AS-IS. 
//    If you did not accept the terms of the license agreement, you are not authorized 
//    to use this sample source code.  For the terms of the license, please see the 
//    license agreement between you and Microsoft. 
﻿//    -------------------------------------------------------------------------------------------- 

 using System;

namespace ConferenceStarterKit.Services
{
    public delegate void LoadEventHandler(object sender, LoadEventArgs e);

    public class LoadEventArgs : EventArgs
    {
        public LoadEventArgs():base(){}

        public LoadEventArgs(bool IsLoaded, string Message) 
        {
            this.IsLoaded = IsLoaded;
            this.Message = Message;
        }
        public bool IsLoaded{get; set;}
        public string Message{get; set;}
    }
}
