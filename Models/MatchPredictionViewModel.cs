namespace Football.Web.Models
{
    // ViewModel pentru formularul de predicție Over/Under 2.5
    public class MatchPredictionViewModel
    {
        // Poți adăuga [Required] / [Range] mai târziu dacă vrei validare
        public float HomeGoalsForAvg { get; set; }
        public float HomeGoalsAgainstAvg { get; set; }
        public float AwayGoalsForAvg { get; set; }
        public float AwayGoalsAgainstAvg { get; set; }
        public float HomeOver25Rate { get; set; }
        public float AwayOver25Rate { get; set; }

        // Rezultat (opțional, pentru afișare în același view)
        public bool? IsOver25 { get; set; }
        public float? Score { get; set; }
    }
}
