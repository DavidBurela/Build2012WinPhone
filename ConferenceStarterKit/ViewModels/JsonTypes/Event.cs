// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using Newtonsoft.Json.Linq;
using JsonCSharpClassGenerator;

namespace ConferenceStarterKit.JsonTypes
{

    internal class Event
    {

        private JObject __jobject;
        public Event(JObject obj)
        {
            this.__jobject = obj;
        }

        public int? SponsorId
        {
            get
            {
                return JsonClassHelper.ReadNullableInteger(JsonClassHelper.GetJToken<JValue>(__jobject, "sponsor_id"));
            }
        }

        public string Name
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "name"));
            }
        }

        public int PrimaryTagId
        {
            get
            {
                return JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(__jobject, "primary_tag_id"));
            }
        }

        public int TimeSlotId
        {
            get
            {
                return JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(__jobject, "time_slot_id"));
            }
        }

        public string ImageUrl
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "image_url"));
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
