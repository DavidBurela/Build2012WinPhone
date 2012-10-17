using System;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using ConferenceStarterKit.Services;

namespace ConferenceStarterKit.ViewModels
{
    public class SpeakerViewModel : ViewModelBase
    {
        public SpeakerItemModel Speaker { get; private set; }
        public ObservableCollection<TwitterStatusItemModel> Twitter { get; private set; }

        public SpeakerViewModel()
        {
            LoadData();
        }

        private IConferenceService _Service;
        public IConferenceService Service
        {
            get
            {
                if (_Service == null)
                {
                    if (IsInDesignMode())
                        _Service = new ConferenceServiceMock();
                    else
                        _Service = new ConferenceService();
                }
                return _Service;
            }
            set
            {
                _Service = value;
            }
        }

        public void LoadData()
        {
            Speaker = App.CurrentSpeaker;
            Speaker.Sessions = App.Sessions.Where(p => p.SpeakerIds.Contains(Speaker.Id)).ToObservableCollection();
            Twitter = Service.GetTwitterFeed(Speaker.Twitter);
        }   
    }
}
