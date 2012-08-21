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

namespace ConferenceStarterKit.ViewModels
{
    public class TwitterStatusItemModel : ModelBase
    {
        private string _date;
        public string Date
        {
            get { return _date; }
            set
            {
                if (_date == value)
                    return;
                _date = value;
                NotifyPropertyChanged("Date");
            }
        }

        private string _text;
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                if (value != _text)
                {
                    _text = value;
                    NotifyPropertyChanged("Text");
                }
            }
        }
    }
}

