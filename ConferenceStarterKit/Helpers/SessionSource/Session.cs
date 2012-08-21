// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using Newtonsoft.Json.Linq;
using ETE.Helpers;

namespace ETE.JsonTypes
{

    internal class Session
    {

        private JObject __jobject;
        public Session(JObject obj)
        {
            this.__jobject = obj;
        }

        public object SponsorId
        {
            get
            {
                return JsonClassHelper.ReadObject(JsonClassHelper.GetJToken<JToken>(__jobject, "sponsor_id"));
            }
        }

        public string Name
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "name"));
            }
        }

        public string SessionLocationName
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "session_location_name"));
            }
        }

        public int PrimaryTagId
        {
            get
            {
                return JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(__jobject, "primary_tag_id"));
            }
        }

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private int[] _speakerIds;
        public int[] SpeakerIds
        {
            get
            {
                if(_speakerIds == null)
                    _speakerIds = (int[])JsonClassHelper.ReadArray<int>(JsonClassHelper.GetJToken<JArray>(__jobject, "speaker_ids"), JsonClassHelper.ReadInteger, typeof(int[]));
                return _speakerIds;
            }
        }

        public int TimeSlotId
        {
            get
            {
                return JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(__jobject, "time_slot_id"));
            }
        }

        public int Id
        {
            get
            {
                return JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(__jobject, "id"));
            }
        }

        public string ItemType
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "item_type"));
            }
        }

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private int[] _tagIds;
        public int[] TagIds
        {
            get
            {
                if(_tagIds == null)
                    _tagIds = (int[])JsonClassHelper.ReadArray<int>(JsonClassHelper.GetJToken<JArray>(__jobject, "tag_ids"), JsonClassHelper.ReadInteger, typeof(int[]));
                return _tagIds;
            }
        }

        public string Description
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "description"));
            }
        }

    }
}
