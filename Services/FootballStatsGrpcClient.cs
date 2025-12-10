using Football.GrpcService;
using FootballStats;        // tipurile generate din footballstats.proto
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;

namespace Football.Web.Services
{
    public interface IFootballStatsGrpcClient
    {
        Task<LastNMatchesResponse> GetLastNMatchesAsync(
            int teamId,
            int lastN,
            CancellationToken cancellationToken = default);

        Task<TeamFormResponse> GetTeamFormAsync(
            int teamId,
            int lastN,
            CancellationToken cancellationToken = default);

        Task<HeadToHeadResponse> GetHeadToHeadAsync(
            int homeTeamId,
            int awayTeamId,
            int lastN,
            CancellationToken cancellationToken = default);
    }

    public class FootballStatsGrpcClient : IFootballStatsGrpcClient
    {
        private readonly FootballStats.FootballStatsClient _client;

        public FootballStatsGrpcClient(IConfiguration configuration)
        {
            var address = configuration["Grpc:FootballStatsUrl"]
                          ?? throw new InvalidOperationException(
                              "Missing configuration value 'Grpc:FootballStatsUrl'.");

            // Canal gRPC către serviciul nostru – în dev permitem certificatul localhost
            var channel = GrpcChannel.ForAddress(address, new GrpcChannelOptions
            {
                HttpHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback =
                        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                }
            });

            _client = new FootballStats.FootballStatsClient(channel);
        }

        public async Task<LastNMatchesResponse> GetLastNMatchesAsync(
            int teamId,
            int lastN,
            CancellationToken cancellationToken = default)
        {
            var request = new LastNMatchesRequest
            {
                TeamId = teamId,
                LastN = lastN
            };

            return await _client.GetLastNMatchesAsync(request, cancellationToken: cancellationToken);
        }

        public async Task<TeamFormResponse> GetTeamFormAsync(
            int teamId,
            int lastN,
            CancellationToken cancellationToken = default)
        {
            var request = new TeamFormRequest
            {
                TeamId = teamId,
                LastN = lastN
            };

            return await _client.GetTeamFormAsync(request, cancellationToken: cancellationToken);
        }

        public async Task<HeadToHeadResponse> GetHeadToHeadAsync(
            int homeTeamId,
            int awayTeamId,
            int lastN,
            CancellationToken cancellationToken = default)
        {
            var request = new HeadToHeadRequest
            {
                HomeTeamId = homeTeamId,
                AwayTeamId = awayTeamId,
                LastN = lastN
            };

            return await _client.GetHeadToHeadAsync(request, cancellationToken: cancellationToken);
        }
    }
}
