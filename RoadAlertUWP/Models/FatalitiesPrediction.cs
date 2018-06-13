using Microsoft.ML.Runtime.Api;

namespace RoadAlertUWP.Models
{
    public class FatalitiesPrediction
    {
        [ColumnName("Score")]
        public float InjurySeverity;
    }
}
