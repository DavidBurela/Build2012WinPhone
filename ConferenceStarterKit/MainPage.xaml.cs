using System.Windows;
using System;
using Microsoft.Phone.Controls;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Collections.ObjectModel;
using ConferenceStarterKit.ViewModels;
using Coding4Fun.Phone.Controls;
using System.Linq;

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

        private void ListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            App.CurrentSpeaker = (SpeakerItemModel)SpeakerList.SelectedItem;
            NavigationService.Navigate(new System.Uri("/SpeakerPage.xaml", System.UriKind.Relative));
        }

        private void SessionListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            App.CurrentSession = (SessionItemModel)lb.SelectedItem;
            NavigationService.Navigate(new System.Uri("/SessionPage.xaml", System.UriKind.Relative));
        }


        private LongListSelector currentSelector;
        private void LongListSelector_GroupViewOpened(object sender, GroupViewOpenedEventArgs e)
        {
            //Hold a reference to the active long list selector.
            currentSelector = sender as LongListSelector;

            //Construct and begin a swivel animation to pop in the group view.
            IEasingFunction quadraticEase = new QuadraticEase { EasingMode = EasingMode.EaseOut };
            Storyboard _swivelShow = new Storyboard();
            ItemsControl groupItems = e.ItemsControl;

            foreach (var item in groupItems.Items)
            {
                UIElement container = groupItems.ItemContainerGenerator.ContainerFromItem(item) as UIElement;
                if (container != null)
                {
                    Border content = VisualTreeHelper.GetChild(container, 0) as Border;
                    if (content != null)
                    {
                        DoubleAnimationUsingKeyFrames showAnimation = new DoubleAnimationUsingKeyFrames();

                        EasingDoubleKeyFrame showKeyFrame1 = new EasingDoubleKeyFrame();
                        showKeyFrame1.KeyTime = TimeSpan.FromMilliseconds(0);
                        showKeyFrame1.Value = -60;
                        showKeyFrame1.EasingFunction = quadraticEase;

                        EasingDoubleKeyFrame showKeyFrame2 = new EasingDoubleKeyFrame();
                        showKeyFrame2.KeyTime = TimeSpan.FromMilliseconds(85);
                        showKeyFrame2.Value = 0;
                        showKeyFrame2.EasingFunction = quadraticEase;

                        showAnimation.KeyFrames.Add(showKeyFrame1);
                        showAnimation.KeyFrames.Add(showKeyFrame2);

                        Storyboard.SetTargetProperty(showAnimation, new PropertyPath(PlaneProjection.RotationXProperty));
                        Storyboard.SetTarget(showAnimation, content.Projection);

                        _swivelShow.Children.Add(showAnimation);
                    }
                }
            }

            _swivelShow.Begin();
        }

        private void LongListSelector_GroupViewClosing(object sender, GroupViewClosingEventArgs e)
        {
            //Cancelling automatic closing and scrolling to do it manually.
            e.Cancel = true;
            if (e.SelectedGroup != null)
            {
                currentSelector.ScrollToGroup(e.SelectedGroup);
            }

            //Dispatch the swivel animation for performance on the UI thread.
            Dispatcher.BeginInvoke(() =>
            {
                //Construct and begin a swivel animation to pop out the group view.
                IEasingFunction quadraticEase = new QuadraticEase { EasingMode = EasingMode.EaseOut };
                Storyboard _swivelHide = new Storyboard();
                ItemsControl groupItems = e.ItemsControl;

                foreach (var item in groupItems.Items)
                {
                    UIElement container = groupItems.ItemContainerGenerator.ContainerFromItem(item) as UIElement;
                    if (container != null)
                    {
                        Border content = VisualTreeHelper.GetChild(container, 0) as Border;
                        if (content != null)
                        {
                            DoubleAnimationUsingKeyFrames showAnimation = new DoubleAnimationUsingKeyFrames();

                            EasingDoubleKeyFrame showKeyFrame1 = new EasingDoubleKeyFrame();
                            showKeyFrame1.KeyTime = TimeSpan.FromMilliseconds(0);
                            showKeyFrame1.Value = 0;
                            showKeyFrame1.EasingFunction = quadraticEase;

                            EasingDoubleKeyFrame showKeyFrame2 = new EasingDoubleKeyFrame();
                            showKeyFrame2.KeyTime = TimeSpan.FromMilliseconds(125);
                            showKeyFrame2.Value = 90;
                            showKeyFrame2.EasingFunction = quadraticEase;

                            showAnimation.KeyFrames.Add(showKeyFrame1);
                            showAnimation.KeyFrames.Add(showKeyFrame2);

                            Storyboard.SetTargetProperty(showAnimation, new PropertyPath(PlaneProjection.RotationXProperty));
                            Storyboard.SetTarget(showAnimation, content.Projection);

                            _swivelHide.Children.Add(showAnimation);
                        }
                    }
                }

                _swivelHide.Completed += _swivelHide_Completed;
                _swivelHide.Begin();

            });
        }

        private void _swivelHide_Completed(object sender, EventArgs e)
        {
            //Close group view.
            if (currentSelector != null)
            {
                currentSelector.CloseGroupView();
                currentSelector = null;
            }
        }


        // hack until we get the data loading completly
        int _temp = 0;
        private void Panorama_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_temp == 0)
            {
                MainViewModel vm = (MainViewModel)LayoutRoot.DataContext;

                //doing it by locationf or now
                var sessionByTimeSlot = from session in vm.Sessions
                                        group session by session.Date.ToString() into session
                                        orderby session.Key ascending
                                        select new Group<SessionItemModel>(session.Key, session);

                //SessionByTime.ItemsSource = sessionByTimeSlot;
                _temp++;
            }

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


        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            InputPrompt input = new InputPrompt();
            input.Completed += new EventHandler<PopUpEventArgs<string, PopUpResult>>(input_Completed);
            input.Title = "Search Sessions";
            input.Message = "enter a keyword";
            input.Show();
        }
        SessionItemModel[] sessionTemp;
        void input_Completed(object sender, PopUpEventArgs<string, PopUpResult> e)
        {
            MainViewModel vm = (MainViewModel)LayoutRoot.DataContext;

            vm.Filter = e.Result;

            if (sessionTemp == null)
            {
                sessionTemp = new SessionItemModel[vm.Sessions.Count];
                vm.Sessions.CopyTo(sessionTemp, 0);
            }

            var filter = from s in sessionTemp.ToList()
                         where s.Description.Contains(e.Result)
                         select s;

            var filterlist = filter.ToList();

            vm.Sessions.Clear();
            vm.Sessions = filterlist.ToList().ToObservableCollection(vm.Sessions);
        }

        private void CloseFilter_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel vm = (MainViewModel)LayoutRoot.DataContext;
            vm.Sessions.Clear();
            vm.Sessions = sessionTemp.ToList().ToObservableCollection(vm.Sessions);
            vm.Filter = string.Empty;
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
                //App.CurrentSession = (SessionItemModel)lb.SelectedItem;
                NavigationService.Navigate(new System.Uri("/SessionPage.xaml", System.UriKind.Relative));
        }

        private void XamList_ItemClicked(object sender, Infragistics.Controls.Grids.ListItemEventArgs e)
        {
            App.CurrentSession = e.Item.Data as SessionItemModel;
            if (App.CurrentSession != null)
                NavigationService.Navigate(new System.Uri("/SessionPage.xaml", System.UriKind.Relative));
        }
    }
}