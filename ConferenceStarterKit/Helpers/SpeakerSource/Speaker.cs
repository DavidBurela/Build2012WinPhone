// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using Newtonsoft.Json.Linq;
using ETE.Helpers;

namespace ETE.JsonTypes
{

    internal class Speaker
    {

        private JObject __jobject;
        public Speaker(JObject obj)
        {
            this.__jobject = obj;
        }

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private object[] _sessionIds;
        public object[] SessionIds
        {
            get
            {
                if(_sessionIds == null)
                    _sessionIds = (object[])JsonClassHelper.ReadArray<object>(JsonClassHelper.GetJToken<JArray>(__jobject, "session_ids"), JsonClassHelper.ReadObject, typeof(object[]));
                return _sessionIds;
            }
        }

        public string Position
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "position"));
            }
        }

        public string PictureUrl
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "picture_url"));
            }
        }

        public int Id
        {
            get
            {
                return JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(__jobject, "id"));
            }
        }

        public string LastName
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "last_name"));
            }
        }

        public string Name
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "name"));
            }
        }

        public string Bio
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "bio"));
            }
        }

        public string Twitter
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "twitter"));
            }
        }

        public string FirstName
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "first_name"));
            }
        }

    }
}
