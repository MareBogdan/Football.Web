namespace Football.Web.Services
{
    // Ce trimitem din MVC către Football.PredictionApi
    // Proprietățile trebuie să aibă ACELEAȘI nume ca în MatchPredictionRequest din PredictionApi
    public class MatchPredictionRequestDto
    {
        public float HomeGoalsForAvg { get; set; }
        public float HomeGoalsAgainstAvg { get; set; }
        public float AwayGoalsForAvg { get; set; }
        public float AwayGoalsAgainstAvg { get; set; }
        public float HomeOver25Rate { get; set; }
        public float AwayOver25Rate { get; set; }
    }
}
