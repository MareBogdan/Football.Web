using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Football.Web.Models;

public class Season
{
    public int Id { get; set; }
    public int YearStart { get; set; }
    public int YearEnd { get; set; }

    public int LeagueId { get; set; }

    public League? League { get; set; }   // <= aici am pus ?

    public ICollection<Match> Matches { get; set; } = new List<Match>();
    public ICollection<TeamSeasonStats> TeamSeasonStats { get; set; } = new List<TeamSeasonStats>();
}
