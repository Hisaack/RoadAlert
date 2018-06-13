using Microsoft.ML.Runtime.Api;

namespace RoadAlertUWP.Models
{
    public class Fatalities
    {
        [Column("0")]
        public string Speed;

        [Column("1")]
        public string Airbag;

        [Column("2")]
        public string SeatBelt;

        [Column("3")]
        public float Frontal;

        [Column("4")]
        public string Sex;

        [Column("5")]
        public float Age;

        [Column("6")]
        public float Year;

        [Column("7")]
        public float Deploy;

        [Column("8")]
        public float InjurySeverity;
    }
}
