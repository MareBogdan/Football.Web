using Football.Web.Data;
using Football.Web.Models;
using Football.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Football.Web.Controllers
{
    public class PredictionsController : Controller
    {
        private readonly IMatchPredictionService _matchPredictionService;
        private readonly ApplicationDbContext _context;

        public PredictionsController(IMatchPredictionService matchPredictionService, ApplicationDbContext context)
        {
            _matchPredictionService = matchPredictionService;
            _context = context;
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
        [HttpGet]
        public async Task<IActionResult> History()
        {
            var history = await _context.PredictionHistories
                .OrderByDescending(ph => ph.CreatedAt)
                .ToListAsync();

            return View(history);
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
            // Salvăm în PredictionHistory
            var historyEntry = new PredictionHistory
            {
                MatchId = null, // deocamdată nu legăm de un Match anume
                CreatedAt = DateTime.UtcNow,
                ModelType = "OverUnder25",
                HomeTeamName = model.HomeTeamName ?? string.Empty,
                AwayTeamName = model.AwayTeamName ?? string.Empty,
                PredictedLabel = result.IsOver25 ? "Over 2.5" : "Under 2.5",
                PredictedProbability = result.Score,
                WasCorrect = null
            };

            _context.PredictionHistories.Add(historyEntry);
            await _context.SaveChangesAsync();


            return View(model);
        }
    }
}
