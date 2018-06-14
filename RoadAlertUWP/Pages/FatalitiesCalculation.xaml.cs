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
using RoadAlertUWP.MLStudioPrediction.Model;
using RoadAlertUWP.MLStudioPrediction.Prediction;
using RoadAlertUWP.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace RoadAlertUWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FatalitiesCalculation : Page
    {
        private  List<ResultGrigViewItem> _gridViewItems;
        public FatalitiesCalculation()
        {
            this.InitializeComponent();
            _gridViewItems = new List<ResultGrigViewItem>()
            {
                new ResultGrigViewItem
                {
                    Name = "Azure Machine Learning Studio",
                    Description = "Get results from Azure ML Studio which is  a GUI-based integrated development environment for constructing and operationalizing Machine Learning workflow on Azure, from which the model is deployed as a web service afetr the training . An internet connection is required for this.",
                    Location = "ms-appx:///Assets/AzureMlStudio.png"
                },
                new ResultGrigViewItem
                {
                    Name = "ML.NET",
                    Description ="Get results from ML.NET which is a free, open-source, and cross-platform machine learning framework that enables you to build custom machine learning solutions and integrate them into your .NET applications. The model runs locally no internet connection is required but Patience is.",
                    Location = "ms-appx:///Assets/mlnet.png"
                }
            };

        }

        private void Speed_Click(object sender, RoutedEventArgs e)
        {
            var item = (MenuFlyoutItem) sender;
            SpeedValueTextBox.Text = item.Text;
        }

        private async void CalculateBtn_Click(object sender, RoutedEventArgs e)
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
            //var model = PredictionModel
            //    .ReadAsync<Fatalities, FatalitiesPrediction>("Model.zip").Result;
            //var prediction = model.Predict(fatality);
            //DummyTxt.Text = prediction.InjurySeverity.ToString();
            var f = new AzureMlFatality()
            {
                Speed = "25-39",
                Age = 26,
                Airbag = "none",
                Seatbelt = "belted",
                Deploy = 1,
                Gender = "f",
                Year = "1990",
                Frontal = 0
            };
           // DummyTxt2.Text = await  new AzureMlPrediction(f).InvokeRequestResponseService();
        }

        private void ResultsGridView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}
