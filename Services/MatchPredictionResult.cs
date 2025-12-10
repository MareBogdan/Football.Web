namespace Football.Web.Services
{
    // Răspunsul “curat” pe care îl folosim în MVC (controller + view)
    public class MatchPredictionResult
    {
        // true = Over 2.5, false = Under 2.5
        public bool IsOver25 { get; set; }

        // Scor / probabilitate pentru Over (de ex. 0.78)
        public float Score { get; set; }
    }
}
