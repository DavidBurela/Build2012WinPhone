// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using Newtonsoft.Json.Linq;
using JsonCSharpClassGenerator;

namespace ConferenceStarterKit.JsonTypes
{

    internal class Conference
    {

        private JObject __jobject;
        public Conference(JObject obj)
        {
            this.__jobject = obj;
        }

        public string StreetAddress
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "street_address"));
            }
        }

        public string State
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "state"));
            }
        }

        public string EndDate
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "end_date"));
            }
        }

        public string City
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "city"));
            }
        }

        public string TwitterHashtag
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "twitter_hashtag"));
            }
        }

        public double Latitude
        {
            get
            {
                return JsonClassHelper.ReadFloat(JsonClassHelper.GetJToken<JValue>(__jobject, "latitude"));
            }
        }

        public string Zip
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "zip"));
            }
        }

        public string ImageUrl
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "image_url"));
            }
        }

        public string Venue
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "venue"));
            }
        }

        public string VenueWebsite
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "venue_website"));
            }
        }

        public double Longitude
        {
            get
            {
                return JsonClassHelper.ReadFloat(JsonClassHelper.GetJToken<JValue>(__jobject, "longitude"));
            }
        }

        public string ConferenceName
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "conference_name"));
            }
        }

        public string VenuePhone
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "venue_phone"));
            }
        }

        public string StartDate
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "start_date"));
            }
        }

    }
}
