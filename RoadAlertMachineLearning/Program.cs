using System;
using System.Threading.Tasks;
using RoadAlertMachineLearning.Pipeline;

namespace RoadAlertMachineLearning
{
    class Program
    {
        static async Task  Main(string[] args)
        {
            await Train();
        }

        private static async Task Train()
        {
            var roadAlertMl = new RoadAlertMl();
            var metrics = roadAlertMl.EvaluateModel(await roadAlertMl.TrainModel());
            Console.WriteLine($"The RMS is {metrics.Rms} and the RSQuared is {metrics.RSquared}");
        }
    }
}
