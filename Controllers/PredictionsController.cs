using System.Threading.Tasks;
using Football.Web.Models;
using Football.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Football.Web.Controllers
{
    public class PredictionsController : Controller
    {
        private readonly IMatchPredictionService _matchPredictionService;

        public PredictionsController(IMatchPredictionService matchPredictionService)
        {
            _matchPredictionService = matchPredictionService;
        }

        // GET: /Predictions/
        [HttpGet]
        public IActionResult Index()
        {
            // Inițial afișăm formularul gol
            var model = new MatchPredictionViewModel
            {
                HomeGoalsForAvg = 1.5f,
                HomeGoalsAgainstAvg = 1.0f,
                AwayGoalsForAvg = 1.3f,
                AwayGoalsAgainstAvg = 1.2f,
                HomeOver25Rate = 0.6f,
                AwayOver25Rate = 0.5f
            };

            return View(model);
        }

        // POST: /Predictions/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(MatchPredictionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Dacă adaugi validare mai târziu, rămânem pe formular
                return View(model);
            }

            // Mapăm ViewModel -> DTO pentru serviciu
            var requestDto = new MatchPredictionRequestDto
            {
                HomeGoalsForAvg = model.HomeGoalsForAvg,
                HomeGoalsAgainstAvg = model.HomeGoalsAgainstAvg,
                AwayGoalsForAvg = model.AwayGoalsForAvg,
                AwayGoalsAgainstAvg = model.AwayGoalsAgainstAvg,
                HomeOver25Rate = model.HomeOver25Rate,
                AwayOver25Rate = model.AwayOver25Rate
            };

            var result = await _matchPredictionService.PredictOverUnder25Async(requestDto);

            // Populăm rezultatul în ViewModel ca să-l afișăm în aceeași pagină
            model.IsOver25 = result.IsOver25;
            model.Score = result.Score;

            return View(model);
        }
    }
}
