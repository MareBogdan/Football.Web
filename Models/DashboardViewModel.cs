using System.Collections.Generic;

namespace Football.Web.Models
{
    public class DashboardViewModel
    {
        // statistici simple
        public int TotalPredictions { get; set; }
        public int OverCount { get; set; }
        public int UnderCount { get; set; }

        public int CorrectCount { get; set; }
        public int WrongCount { get; set; }
        public int UnknownCount { get; set; }

        public double OverPercentage { get; set; }
        public double UnderPercentage { get; set; }
        public double SuccessRate { get; set; }

        // pentru grafic "predictions per day"
        public List<string> PredictionsPerDayLabels { get; set; } = new();
        public List<int> PredictionsPerDayCounts { get; set; } = new();
    }
}
