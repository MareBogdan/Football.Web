using Football.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Football.Web.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<League> Leagues { get; set; } = null!;
    public DbSet<Season> Seasons { get; set; } = null!;
    public DbSet<Team> Teams { get; set; } = null!;
    public DbSet<Player> Players { get; set; } = null!;
    public DbSet<Match> Matches { get; set; } = null!;
    public DbSet<TeamSeasonStats> TeamSeasonStats { get; set; } = null!;
    public DbSet<PredictionHistory> PredictionHistories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Team>()
            .HasOne(t => t.League)
            .WithMany(l => l.Teams)
            .HasForeignKey(t => t.LeagueId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Season>()
            .HasOne(s => s.League)
            .WithMany(l => l.Seasons)
            .HasForeignKey(s => s.LeagueId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Match>()
            .HasOne(m => m.HomeTeam)
            .WithMany(t => t.HomeMatches)
            .HasForeignKey(m => m.HomeTeamId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Match>()
            .HasOne(m => m.AwayTeam)
            .WithMany(t => t.AwayMatches)
            .HasForeignKey(m => m.AwayTeamId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Match>()
            .HasOne(m => m.Season)
            .WithMany(s => s.Matches)
            .HasForeignKey(m => m.SeasonId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TeamSeasonStats>()
            .HasOne(ts => ts.Team)
            .WithMany(t => t.TeamSeasonStats)
            .HasForeignKey(ts => ts.TeamId);

        modelBuilder.Entity<TeamSeasonStats>()
            .HasOne(ts => ts.Season)
            .WithMany(s => s.TeamSeasonStats)
            .HasForeignKey(ts => ts.SeasonId);

        modelBuilder.Entity<PredictionHistory>()
            .HasOne(ph => ph.Match)
            .WithMany(m => m.Predictions)
            .HasForeignKey(ph => ph.MatchId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
