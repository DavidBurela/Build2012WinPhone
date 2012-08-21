// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using Newtonsoft.Json.Linq;
using JsonCSharpClassGenerator;

namespace ConferenceStarterKit.JsonTypes
{

    internal class Tag
    {

        private JObject __jobject;
        public Tag(JObject obj)
        {
            this.__jobject = obj;
        }

        public string Name
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "name"));
            }
        }

        public int Id
        {
            get
            {
                return JsonClassHelper.ReadInteger(JsonClassHelper.GetJToken<JValue>(__jobject, "id"));
            }
        }

        public string Color
        {
            get
            {
                return JsonClassHelper.ReadString(JsonClassHelper.GetJToken<JValue>(__jobject, "color"));
            }
        }

    }
}
