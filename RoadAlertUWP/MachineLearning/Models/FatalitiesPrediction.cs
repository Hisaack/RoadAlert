using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Runtime.Api;

namespace MachineLearning.Model
{
    public class FatalitiesPrediction
    {
        [ColumnName("Score")]
        public int InjurySeverity;
    }
}
