// JSON C# Class Generator
// http://at-my-window.blogspot.com/?page=json-class-generator

using System;
using Newtonsoft.Json.Linq;
using JsonCSharpClassGenerator;
using ConferenceStarterKit.JsonTypes;

namespace ConferenceStarterKit
{

    internal class ConferenceResponse
    {

        public ConferenceResponse(string json)
         : this(JObject.Parse(json))
        {
        }

        private JObject __jobject;
        public ConferenceResponse(JObject obj)
        {
            this.__jobject = obj;
        }

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private Tag[] _tags;
        public Tag[] Tags
        {
            get
            {
                if(_tags == null)
                    _tags = (Tag[])JsonClassHelper.ReadArray<Tag>(JsonClassHelper.GetJToken<JArray>(__jobject, "tags"), JsonClassHelper.ReadStronglyTypedObject<Tag>, typeof(Tag[]));
                return _tags;
            }
        }

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private Speaker[] _speakers;
        public Speaker[] Speakers
        {
            get
            {
                if(_speakers == null)
                    _speakers = (Speaker[])JsonClassHelper.ReadArray<Speaker>(JsonClassHelper.GetJToken<JArray>(__jobject, "speakers"), JsonClassHelper.ReadStronglyTypedObject<Speaker>, typeof(Speaker[]));
                return _speakers;
            }
        }

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private Conference _conference;
        public Conference Conference
        {
            get
            {
                if(_conference == null)
                    _conference = JsonClassHelper.ReadStronglyTypedObject<Conference>(JsonClassHelper.GetJToken<JObject>(__jobject, "conference"));
                return _conference;
            }
        }

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private Event[] _event;
        public Event[] Event
        {
            get
            {
                if(_event == null)
                    _event = (Event[])JsonClassHelper.ReadArray<Event>(JsonClassHelper.GetJToken<JArray>(__jobject, "event"), JsonClassHelper.ReadStronglyTypedObject<Event>, typeof(Event[]));
                return _event;
            }
        }

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private Sponsor[] _sponsors;
        public Sponsor[] Sponsors
        {
            get
            {
                if(_sponsors == null)
                    _sponsors = (Sponsor[])JsonClassHelper.ReadArray<Sponsor>(JsonClassHelper.GetJToken<JArray>(__jobject, "sponsors"), JsonClassHelper.ReadStronglyTypedObject<Sponsor>, typeof(Sponsor[]));
                return _sponsors;
            }
        }

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private Session[] _sessions;
        public Session[] Sessions
        {
            get
            {
                if(_sessions == null)
                    _sessions = (Session[])JsonClassHelper.ReadArray<Session>(JsonClassHelper.GetJToken<JArray>(__jobject, "sessions"), JsonClassHelper.ReadStronglyTypedObject<Session>, typeof(Session[]));
                return _sessions;
            }
        }

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private TimeSlot[] _timeSlots;
        public TimeSlot[] TimeSlots
        {
            get
            {
                if(_timeSlots == null)
                    _timeSlots = (TimeSlot[])JsonClassHelper.ReadArray<TimeSlot>(JsonClassHelper.GetJToken<JArray>(__jobject, "time_slots"), JsonClassHelper.ReadStronglyTypedObject<TimeSlot>, typeof(TimeSlot[]));
                return _timeSlots;
            }
        }

    }
}
