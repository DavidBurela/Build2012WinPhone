using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Collections.ObjectModel;
using System.Windows.Shapes;
using ConferenceStarterKit.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using Microsoft.Phone.Scheduler;

namespace ConferenceStarterKit
{
    public partial class SessionPage : PhoneApplicationPage
    {
        public SessionPage()
        {
            InitializeComponent();
        }

        private void appbar_pin_Click(object sender, EventArgs e)
        {

        }

        private void appbar_add_Click(object sender, EventArgs e)
        {
            SessionViewModel vm = (SessionViewModel) this.LayoutRoot.DataContext;
            bool found = false;

            foreach (SessionItemModel s in App.SavedSessions)
            {
                if (s.Title.Trim() == vm.Session.Title.Trim())
                {
                    App.SavedSessions.Remove(s);
                    found = false;
                    break;
                }
            }
            App.SavedSessions.Add(vm.Session);

            //only add a reminder one time
            if (!found)
            {
                try
                {
                    Reminder reminder = new Reminder(vm.Session.Title);
                    reminder.BeginTime = vm.Session.Date;
                    reminder.RecurrenceType = RecurrenceInterval.None;

                    ScheduledActionService.Add(reminder);
                }
                catch (Exception)
                {
                   //need to let use know we cound not add a reminder?
                }

            }
        }

        private void appbar_send_Click(object sender, EventArgs e)
        {
            EmailComposeTask email = new EmailComposeTask();
            email.Subject = "This Session at ConferenceStarterKit Might be of Interest";
            email.Body = "";

            email.Show();


        }
    }
}