using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using RoadAlertUWP.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace RoadAlertUWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RoadAlertHome : Page
    {
        private List<HomeMenuItem> homeMenuItems;
        public RoadAlertHome()
        {
            this.InitializeComponent();
            homeMenuItems=new List<HomeMenuItem>();
            InitMenuItems();
            BackButton.Visibility = Visibility.Collapsed;
        }

        private void InitMenuItems()
        {
            homeMenuItems = new List<HomeMenuItem>()
            {
                new HomeMenuItem(){ItemLocation = "ms-appx:///Assets/home.png", ItemName = "Home"},
                new HomeMenuItem(){ItemLocation = "ms-appx:///Assets/road.png", ItemName = "Roads"},
                new HomeMenuItem(){ItemLocation = "ms-appx:///Assets/accident.png", ItemName = "Accident"},
                new HomeMenuItem(){ItemLocation = "ms-appx:///Assets/fatalities.png", ItemName = "Fatalities Calculation"},
                new HomeMenuItem(){ItemLocation = "ms-appx:///Assets/corrupt.png", ItemName = "Bribery Report"}
            };
        }

        private void HamburgerBtn_OnClick(object sender, RoutedEventArgs e)
        {
            RoadAlertSplitView.IsPaneOpen = !RoadAlertSplitView.IsPaneOpen;
        }

        private void SearchRoadBox_OnQuerySubmitted(SearchBox sender, SearchBoxQuerySubmittedEventArgs args)
        {
            throw new NotImplementedException();
        }

        private void HomeMenuListView_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = (HomeMenuItem) e.ClickedItem;
            switch (item.ItemName)
            {
                case "Home":
                    RoadAlertFrame.Navigate(typeof(RoadAlertHome));
                    break;
                case "Roads":
                    RoadAlertFrame.Navigate(typeof(Roads));
                    break;
                case "Accident":
                    RoadAlertFrame.Navigate(typeof(Accidents));
                    break;
                case "Fatalities Calculation":
                    RoadAlertFrame.Navigate(typeof(Roads));
                    break;
                case "Bribery Report":
                    RoadAlertFrame.Navigate(typeof(BriberyReport));
                    break;
            }
        }
    }
}
