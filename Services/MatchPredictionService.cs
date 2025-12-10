using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Football.Web.Services
{
    public class MatchPredictionService : IMatchPredictionService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MatchPredictionService> _logger;

        public MatchPredictionService(HttpClient httpClient, ILogger<MatchPredictionService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<MatchPredictionResult> PredictOverUnder25Async(
            MatchPredictionRequestDto request,
            CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            // Endpoint-ul din Football.PredictionApi:
            // noi l-am definit ca POST /api/MatchPrediction
            var url = "api/MatchPrediction";

            try
            {
                var response = await _httpClient.PostAsJsonAsync(url, request, cancellationToken);
                response.EnsureSuccessStatusCode();

                var result =
                    await response.Content.ReadFromJsonAsync<MatchPredictionResult>(cancellationToken: cancellationToken);

                if (result == null)
                {
                    throw new InvalidOperationException("Empty response from prediction API.");
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling prediction API.");
                // Poți alege să arunci mai departe sau să întorci un rezultat “fallback”
                throw;
            }
        }
    }
}
