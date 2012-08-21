// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using Newtonsoft.Json.Linq;
using ETE.Helpers;
using ETE.JsonTypes;

namespace ETE
{

    internal class SpeakerResponse
    {

        public SpeakerResponse(string json)
         : this(JObject.Parse(json))
        {
        }

        private JObject __jobject;
        public SpeakerResponse(JObject obj)
        {
            this.__jobject = obj;
        }

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private Speaker[] _speakers;
        public Speaker[] Speakers
        {
            get
            {
                if(_speakers == null)
                    _speakers = (Speaker[])JsonClassHelper.ReadArray<Speaker>(JsonClassHelper.GetJToken<JArray>(__jobject, "Speakers"), JsonClassHelper.ReadStronglyTypedObject<Speaker>, typeof(Speaker[]));
                return _speakers;
            }
        }

    }
}
