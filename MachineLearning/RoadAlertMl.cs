using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MachineLearning.Model;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;

namespace MachineLearning
{
    public class RoadAlertMl
    {
        //path to dataset to train the model
        private const string Datapath = @".\Data\RoadAlertTrainDataSet.csv";
        //path to dataset to evaluate the model
        private const string TestDataPath= @".\Data\RoadAlertTestDataSet.csv";
        //path to storage of the trained model
        private const string ModelPath = @".\Data\Model.zip";

        public static async Task<PredictionModel<Fatalities, FatalitiesPrediction>> TrainModel()
        {
            var  pipeline=new LearningPipeline();
            pipeline.Add(new TextLoader(Datapath).CreateFrom<Fatalities>(separator:','));
            pipeline.Add(new ColumnCopier("InjurySeverity", "Label"));
            pipeline.Add(new CategoricalOneHotVectorizer("Speed","Airbag", "SeatBelt", "Sex"));
            pipeline.Add(new ColumnConcatenator("Features", "Speed", "Airbag", "SeatBelt", "Frontal", "Sex", "Age", "Year", "Deploy"));
            pipeline.Add(new FastTreeRegressor());
            PredictionModel<Fatalities, FatalitiesPrediction>
                model = pipeline.Train<Fatalities, FatalitiesPrediction>();
            await model.WriteAsync(ModelPath);
            return model;
        }
        


    }
}
