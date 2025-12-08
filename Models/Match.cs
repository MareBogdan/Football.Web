namespace Football.Web.Models;

public class Match
{
    public int Id { get; set; }

    public int SeasonId { get; set; }
    public Season Season { get; set; } = null!;

    public int HomeTeamId { get; set; }
    public Team HomeTeam { get; set; } = null!;

    public int AwayTeamId { get; set; }
    public Team AwayTeam { get; set; } = null!;

    public DateTime KickoffDate { get; set; }

    public int? HomeGoals { get; set; }
    public int? AwayGoals { get; set; }

    public string? Result1X2 { get; set; }
    public bool? IsOver25 { get; set; }

    public ICollection<PredictionHistory> Predictions { get; set; } = new List<PredictionHistory>();
}
