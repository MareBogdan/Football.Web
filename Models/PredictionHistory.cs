namespace Football.Web.Models;

public class PredictionHistory
{
    public int Id { get; set; }

    public int? MatchId { get; set; }
    public Match? Match { get; set; }

    public DateTime CreatedAt { get; set; }

    public string ModelType { get; set; } = string.Empty;
    public string HomeTeamName { get; set; } = string.Empty;
    public string AwayTeamName { get; set; } = string.Empty;

    public string PredictedLabel { get; set; } = string.Empty;
    public float PredictedProbability { get; set; }
    public bool? WasCorrect { get; set; }
}
