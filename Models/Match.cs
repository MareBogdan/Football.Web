using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Football.Web.Models;

public class Match
{
    public int Id { get; set; }

    public int SeasonId { get; set; }
    [ValidateNever]
    public Season? Season { get; set; }

    public int HomeTeamId { get; set; }
    [ValidateNever]
    public Team? HomeTeam { get; set; }

    public int AwayTeamId { get; set; }
    [ValidateNever]
    public Team? AwayTeam { get; set; }

    public DateTime KickoffDate { get; set; }

    public int? HomeGoals { get; set; }
    public int? AwayGoals { get; set; }

    public string? Result1X2 { get; set; }
    public bool? IsOver25 { get; set; }

    [ValidateNever]
    public ICollection<PredictionHistory> Predictions { get; set; } = new List<PredictionHistory>();
}
