using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadAlertUWP.MLStudioPrediction.Model
{
    public class AzureMlFatality
    {
        public string Speed { get; set; }
        public string Airbag { get; set; }
        public string Seatbelt { get; set; }
        public int Frontal { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Year { get; set; }
        public int Deploy { get; set; }
        public float Score { get; set; }
    }
}
