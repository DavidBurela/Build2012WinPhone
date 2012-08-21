using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using ConferenceStarterKit.Services;
using System.Collections.ObjectModel;

namespace ConferenceStarterKit.ViewModels
{
    public class SessionViewModel : ViewModelBase
    {
        public ObservableCollection<SpeakerItemModel> Speakers { get; private set; }
        public SessionItemModel Session { get; private set; }

        public SessionViewModel()
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
            if (App.CurrentSession != null)
            {
                Session = App.CurrentSession;
                Speakers = Session.Speakers;
            }
        }   
    }
}
