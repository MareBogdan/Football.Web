using Football.GrpcService;

namespace Football.Web.Models
{
    public class TeamFormViewModel
    {
        public Team Team { get; set; } = null!;

        public int LastN { get; set; }

        public TeamFormResponse Form { get; set; } = null!;

        public LastNMatchesResponse LastMatches { get; set; } = null!;
    }
}
