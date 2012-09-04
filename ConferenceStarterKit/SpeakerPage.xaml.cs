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

        private void SessionList_ItemClicked(object sender, Infragistics.Controls.Grids.ListItemEventArgs e)
        {
            App.CurrentSession = e.Item.Data as SessionItemModel;
            if (App.CurrentSession != null)
                NavigationService.Navigate(new System.Uri("/SessionPage.xaml", System.UriKind.Relative));
        }
    }
}