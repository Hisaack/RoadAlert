using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
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
        private AzureMlFatality _azureMlFatality;
        private Fatalities _fatality;
        private string _gender;
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

        private  void CalculateBtn_Click(object sender, RoutedEventArgs e)
        {
             _fatality = new Fatalities()
            {
                Speed = SpeedValueTextBox.Text,
                Age = float.Parse(AgeTextBox.Text),
                Airbag = AirbagToggleSwitch.IsOn ? "airbag" : "none",
                SeatBelt = SeatBeltToggleSwitch.IsOn? "belted":"none",
                Deploy = AirbagDeploymentToggleSwitch.IsOn?1:0,
                Sex = _gender,
                Year = float.Parse(YearTextBox.Text),
                Frontal = FrontalToggleSwitch.IsOn?1:0
            };

             _azureMlFatality = new AzureMlFatality()
            {
                Speed = SpeedValueTextBox.Text,
                Age = int.Parse(AgeTextBox.Text),
                Airbag = AirbagToggleSwitch.IsOn ? "airbag" : "none",
                Seatbelt = SeatBeltToggleSwitch.IsOn ? "belted" : "none",
                Deploy = AirbagDeploymentToggleSwitch.IsOn ? 1 : 0,
                Gender = _gender,
                Year = YearTextBox.Text,
                Frontal = FrontalToggleSwitch.IsOn ? 1 : 0
            };

            ResultsStackPanel.Visibility = Visibility.Visible;
            CalculateBtn.Visibility = Visibility.Collapsed;
            SpeedValueTextBox.Text = "";
            AgeTextBox.Text = "";
            AirbagToggleSwitch.IsOn = false;
            SeatBeltToggleSwitch.IsOn = false;
            AirbagDeploymentToggleSwitch.IsOn = false;
            GenderComboBox.SelectedItem = null;
            YearTextBox.Text = "";
            FrontalToggleSwitch.IsOn = false;
        }

        private void ResultsGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var gridItemClicked = (ResultGrigViewItem)e.ClickedItem;
            switch (gridItemClicked.Name)
            {
                case "Azure Machine Learning Studio":
                    var model = PredictionModel
                        .ReadAsync<Fatalities, FatalitiesPrediction>("Model.zip").Result;
                    var prediction = model.Predict(_fatality);
                    ScoreTxtBlock.Text = prediction.InjurySeverity.ToString("##.000");
                    var injSev = prediction.InjurySeverity.ToString("N");
                    switch (injSev)
                    {
                        case "0":
                            InjurySeverityScaleTxtBlock.Text = "None";
                            break;
                        case "1":
                            InjurySeverityScaleTxtBlock.Text = "Possible injury";
                            break;
                        case "2":
                            InjurySeverityScaleTxtBlock.Text = "No incapacity!";
                            break;
                        case "3":
                            InjurySeverityScaleTxtBlock.Text = "Incapacity!";
                            break;
                        default:
                            InjurySeverityScaleTxtBlock.Text="Fatal!";
                            InjurySeverityScaleTxtBlock.Foreground = new SolidColorBrush(Colors.Red);   
                            break;
                    }

                    InjurySeverityTxtBlock.Text = injSev;
                    break;
                case "ML.NET":
                    break;
            }
        }

        private void LearnSeverityScale_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ModelScoreBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GitHubBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ComputeAgaiBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GenderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = ((ComboBox) sender).SelectedItem;
            if (selectedItem != null)
                _gender = selectedItem.ToString();
        }
    }
}
