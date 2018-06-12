using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MachineLearning;

namespace MlTest
{
    class Program
    {
        public static async void Main(string[] args)
        {
              await Train();
        }

        private static async Task Train()
        {
            var roadMl = new RoadAlertMl();
            var model = await roadMl.TrainModel();
            var metrics = roadMl.EvaluateModel(model);
            Console.WriteLine("Rms=" + metrics.Rms);
            Console.ReadKey();
        }
    }
}
