using System.Threading.Tasks;
using MachineLearningRoadAlert.Models;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Models;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;

namespace RoadAlertMachineLearning.Pipeline
{
    public class RoadAlertMl
    {
        //path to dataset to train the model 
        private const string Datapath = "RoadAlertMachineLearning/Data/RoadAlertTrainDataSet.txt";
        //path to dataset to evaluate the model
        private const string TestDataPath= "RoadAlertMachineLearning/Data/RoadAlertTestDataSet.txt";
        //path to storage of the trained model
        private const string ModelPath = "RoadAlertMachineLearning/Data/Model.zip";

        public  async Task<PredictionModel<Fatalities, FatalitiesPrediction>> TrainModel()
        {
            var pipeline = new LearningPipeline 
            {
                new TextLoader(Datapath).CreateFrom<Fatalities>(separator: ','),
                new ColumnCopier(("InjurySeverity", "Label")),
                new CategoricalOneHotVectorizer("Speed", "Airbag", "SeatBelt", "Sex"),
                new ColumnConcatenator("Features", "Speed", "Airbag", "SeatBelt", "Frontal", "Sex", "Age", "Year",
                    "Deploy"),
                new FastTreeRegressor()
            };
            var model =  pipeline.Train<Fatalities, FatalitiesPrediction>();
            await model.WriteAsync(ModelPath);
            return model;
        }

        public  RegressionMetrics EvaluateModel(PredictionModel<Fatalities, FatalitiesPrediction> model)
        {
            var testData=new TextLoader(TestDataPath).CreateFrom<Fatalities>(useHeader:true, separator:',');
            var evaluator = new RegressionEvaluator();
            RegressionMetrics metrics = evaluator.Evaluate(model, testData);
            return metrics;
        }


    }
}
