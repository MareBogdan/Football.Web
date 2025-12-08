namespace Football.Web.Models;

public class Player
{
    public int Id { get; set; }

    public int TeamId { get; set; }
    public Team Team { get; set; } = null!;

    public string FullName { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public int? ShirtNumber { get; set; }
    public DateTime? BirthDate { get; set; }
}
