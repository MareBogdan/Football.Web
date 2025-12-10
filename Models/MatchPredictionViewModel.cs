namespace Football.Web.Models
{
    public class MatchPredictionViewModel
    {
        // Nume echipe
        public string HomeTeamName { get; set; } = string.Empty;
        public string AwayTeamName { get; set; } = string.Empty;

        // Features pentru model
        public float HomeGoalsForAvg { get; set; }
        public float HomeGoalsAgainstAvg { get; set; }
        public float AwayGoalsForAvg { get; set; }
        public float AwayGoalsAgainstAvg { get; set; }
        public float HomeOver25Rate { get; set; }
        public float AwayOver25Rate { get; set; }

        // Rezultat
        public bool? IsOver25 { get; set; }
        public float? Score { get; set; }
    }
}
