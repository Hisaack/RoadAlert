using Microsoft.ML.Runtime.Api;

namespace MachineLearningRoadAlert.Models
{
    public class FatalitiesPrediction
    {
        [ColumnName("Score")]
        public int InjurySeverity;
    }
}
