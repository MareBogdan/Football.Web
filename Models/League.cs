namespace Football.Web.Models;

public class League
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;

    public ICollection<Season> Seasons { get; set; } = new List<Season>();
    public ICollection<Team> Teams { get; set; } = new List<Team>();
}
