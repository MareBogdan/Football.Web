using System.Numerics;
using System.Text.RegularExpressions;

namespace Football.Web.Models;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;

    public int LeagueId { get; set; }
    public League League { get; set; } = null!;

    public ICollection<Player> Players { get; set; } = new List<Player>();
    public ICollection<Match> HomeMatches { get; set; } = new List<Match>();
    public ICollection<Match> AwayMatches { get; set; } = new List<Match>();
    public ICollection<TeamSeasonStats> TeamSeasonStats { get; set; } = new List<TeamSeasonStats>();
}
