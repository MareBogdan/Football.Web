using System.Threading;
using System.Threading.Tasks;

namespace Football.Web.Services
{
    public interface IMatchPredictionService
    {
        // Va apela REST API-ul ML și va întoarce predicția pentru Over/Under 2.5
        Task<MatchPredictionResult> PredictOverUnder25Async(
            MatchPredictionRequestDto request,
            CancellationToken cancellationToken = default);
    }
}
