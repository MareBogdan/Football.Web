using Football.GrpcService;

namespace Football.Web.Models
{
    public class HeadToHeadViewModel
    {
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public int LastN { get; set; } = 5;

        public Team? HomeTeam { get; set; }
        public Team? AwayTeam { get; set; }

        public HeadToHeadResponse? Stats { get; set; }
    }
}
