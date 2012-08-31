﻿//    -------------------------------------------------------------------------------------------- 
//    Copyright (c) 2011 Microsoft Corporation.  All rights reserved. 
//    Use of this sample source code is subject to the terms of the Microsoft license 
//    agreement under which you licensed this sample source code and is provided AS-IS. 
//    If you did not accept the terms of the license agreement, you are not authorized 
//    to use this sample source code.  For the terms of the license, please see the 
//    license agreement between you and Microsoft. 
﻿//    -------------------------------------------------------------------------------------------- 

 namespace ConferenceStarterKit
{
    public class Settings
    {
        public const string SessionServiceUri = "http://odata.msteched.com/teau12/sessions.svc/Sessions?$format=json";
        //public const string SessionServiceUri = "https://eup84q.blu.livefilestore.com/y1piNwHiDJPW_mojkUSUKd6nfTndoiAT_h6d10zApUKusCfdgEEYUzWmjW6ABJNzX3Tl40cas3I8REzKw6UbzBsbA/ete2011.json?download&psid=1";
        public const string SpeakerServiceUri = "";
        public const string TwitterServiceUri = "http://api.twitter.com/1/statuses/user_timeline.xml?screen_name=";
        public const string ApplicationName = "Emerging Technologies for the Enterprise";
        public const string EmailAddress = "ddiaz@live.com";
        public const string Subject = "please provide feedback";
        public const string DefaulImage = "/Images/defaultimage.png";
    }
}