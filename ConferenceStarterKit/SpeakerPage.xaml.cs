using System.Windows;
using ConferenceStarterKit.ViewModels;
using Microsoft.Phone.Controls;

namespace ConferenceStarterKit
{
    public partial class SpeakerPage : PhoneApplicationPage
    {
        public SpeakerPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            var vm = LayoutRoot.DataContext as SpeakerViewModel;
            if(vm != null && string.IsNullOrWhiteSpace(vm.Speaker.Twitter))
                noTwitterTextBlock.Visibility = Visibility.Visible;
        }

        private void SessionList_ItemClicked(object sender, Infragistics.Controls.Grids.ListItemEventArgs e)
        {
            App.CurrentSession = e.Item.Data as SessionItemModel;
            if (App.CurrentSession != null)
                NavigationService.Navigate(new System.Uri("/SessionPage.xaml", System.UriKind.Relative));
        }
    }
}