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
using Microsoft.ML;
using RoadAlertUWP.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace RoadAlertUWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FatalitiesCalculation : Page
    {
        public FatalitiesCalculation()
        {
            this.InitializeComponent();
        }

        private void Speed_Click(object sender, RoutedEventArgs e)
        {
            var item = (MenuFlyoutItem) sender;
            SpeedValueTextBox.Text = item.Text;
        }

        private  void CalculateBtn_Click(object sender, RoutedEventArgs e)
        {
            ResultsStackPanel.Visibility = Visibility.Visible;
            var fatality = new Fatalities()
            {
                Speed = "25-39",
                Age = 26,
                Airbag = "none",
                SeatBelt = "belted",
                Deploy = 1,
                Sex = "f",
                Year = 1990,
                Frontal = 0
            };
            var model = PredictionModel
                .ReadAsync<Fatalities, FatalitiesPrediction>("Model.zip").Result;
            var prediction = model.Predict(fatality);
            DummyTxt.Text = prediction.InjurySeverity.ToString();
        }
    }
}
