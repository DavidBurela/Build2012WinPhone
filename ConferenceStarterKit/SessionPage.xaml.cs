using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Collections.ObjectModel;
using System.Windows.Shapes;
using ConferenceStarterKit.ViewModels;
using Infragistics.Controls.Interactions;
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
            SessionViewModel vm = (SessionViewModel)this.LayoutRoot.DataContext;
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
                    reminder.BeginTime = vm.Session.Date.AddMinutes(-5); // Add a reminder 5 mins before it starts
                    reminder.Content = vm.Session.Title;
                    reminder.RecurrenceType = RecurrenceInterval.None;

                    ScheduledActionService.Add(reminder);
                    XamMessageBox.Show("Favourite added", "Added to your favourites.",
                        () => { },
                        VerticalPosition.Center,
                        new XamMessageBoxCommand("OK", () => { }));
                }
                catch (Exception)
                {
                    //need to let use know we cound not add a reminder?
                    XamMessageBox.Show("Already a favourite", "This session is already a favourite.",
                        () => { },
                        VerticalPosition.Center,
                        new XamMessageBoxCommand("OK", () => { }));
                }

            }
        }

        private void appbar_send_Click(object sender, EventArgs e)
        {
            var vm = (SessionViewModel)this.LayoutRoot.DataContext;
            var session = vm.Session;

            EmailComposeTask email = new EmailComposeTask();
            email.Subject = "This session at TechEd might be of interest";
            var sb = new StringBuilder();
            sb.Append("Code: ");
            sb.AppendLine(session.Code);
            sb.Append("Title: ");
            sb.AppendLine(session.Title);
            sb.Append("Time: ");
            sb.AppendLine(session.Date.ToShortDateString() + " " + session.Date.ToShortTimeString());
            sb.Append("Room: ");
            sb.AppendLine(session.Location);
            sb.Append("Description: ");
            sb.AppendLine(session.Description);
            email.Body = sb.ToString();

            email.Show();


        }
    }
}