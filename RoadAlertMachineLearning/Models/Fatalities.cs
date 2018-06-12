using Microsoft.ML.Runtime.Api;

namespace MachineLearningRoadAlert.Models
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
        public int Frontal;

        [Column("4")]
        public char Sex;

        [Column("5")]
        public int Age;

        [Column("6")]
        public int Year;

        [Column("7")]
        public int Deploy;
        
    }
}
