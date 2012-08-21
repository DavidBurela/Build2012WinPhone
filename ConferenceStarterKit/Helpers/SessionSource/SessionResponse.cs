// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using Newtonsoft.Json.Linq;
using ETE.Helpers;
using ETE.JsonTypes;

namespace ETE
{

    internal class SessionResponse
    {

        public SessionResponse(string json)
         : this(JObject.Parse(json))
        {
        }

        private JObject __jobject;
        public SessionResponse(JObject obj)
        {
            this.__jobject = obj;
        }

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private Session[] _sessions;
        public Session[] Sessions
        {
            get
            {
                if(_sessions == null)
                    _sessions = (Session[])JsonClassHelper.ReadArray<Session>(JsonClassHelper.GetJToken<JArray>(__jobject, "Sessions"), JsonClassHelper.ReadStronglyTypedObject<Session>, typeof(Session[]));
                return _sessions;
            }
        }

    }
}
