using System;
using System.Windows;
using Microsoft.Phone.Controls;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using ConferenceStarterKit.ViewModels;

namespace ConferenceStarterKit
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        // Load data for the ViewModel Items 
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {


        }

        private void SessionListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            App.CurrentSession = (SessionItemModel)lb.SelectedItem;
            NavigationService.Navigate(new System.Uri("/SessionPage.xaml", System.UriKind.Relative));
        }

        private void contact_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new System.Uri("/Contact.xaml", System.UriKind.Relative));
        }

        private void info_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new System.Uri("/Information.xaml", System.UriKind.Relative));
        }

        private void hotel_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new System.Uri("/Hotel.xaml", System.UriKind.Relative));
        }

        private void transport_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new System.Uri("/Transportation.xaml", System.UriKind.Relative));
        }


        //delete reminders from context menu
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem i = (MenuItem)sender;

            foreach (SessionItemModel s in App.SavedSessions)
            {
                if (s.Title == i.CommandParameter.ToString())
                {
                    App.SavedSessions.Remove(s);
                    break;
                }
            }
        }

        private void SessionList_ItemClicked(object sender, Infragistics.Controls.Grids.ListItemEventArgs e)
        {
            App.CurrentSession = e.Item.Data as SessionItemModel;
            if (App.CurrentSession != null)
                NavigationService.Navigate(new System.Uri("/SessionPage.xaml", System.UriKind.Relative));
        }

        private void ApplicationBarAbout_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/YourLastAboutDialog;component/AboutPage.xaml", UriKind.Relative));
        }

        private void SpeakerList_ItemClicked(object sender, Infragistics.Controls.Grids.ListItemEventArgs e)
        {
            App.CurrentSpeaker = e.Item.Data as SpeakerItemModel;
            if (App.CurrentSpeaker != null)
                NavigationService.Navigate(new System.Uri("/SpeakerPage.xaml", System.UriKind.Relative));
        }
    }
}