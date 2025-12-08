namespace Football.Web.Models
{
    public class Team
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string ShortName { get; set; } = string.Empty;

        public int LeagueId { get; set; }
        public League? League { get; set; }

        // colecțiile le lasam simple, nu ne incurca
        public ICollection<Match> HomeMatches { get; set; } = new List<Match>();
        public ICollection<Match> AwayMatches { get; set; } = new List<Match>();
        public ICollection<TeamSeasonStats> TeamSeasonStats { get; set; } = new List<TeamSeasonStats>();
    }
}
