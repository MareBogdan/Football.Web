using System;
using System.Linq;
using System.Threading.Tasks;
using Football.Web.Data;
using Football.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Football.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var history = await _context.PredictionHistories
                .OrderBy(ph => ph.CreatedAt)
                .ToListAsync();

            var model = new DashboardViewModel();

            if (!history.Any())
            {
                return View(model);
            }

            model.TotalPredictions = history.Count;
            model.OverCount = history.Count(h => h.PredictedLabel.Contains("Over", StringComparison.OrdinalIgnoreCase));
            model.UnderCount = history.Count(h => h.PredictedLabel.Contains("Under", StringComparison.OrdinalIgnoreCase));

            model.CorrectCount = history.Count(h => h.WasCorrect == true);
            model.WrongCount = history.Count(h => h.WasCorrect == false);
            model.UnknownCount = history.Count(h => h.WasCorrect == null);

            if (model.TotalPredictions > 0)
            {
                model.OverPercentage = model.OverCount * 100.0 / model.TotalPredictions;
                model.UnderPercentage = model.UnderCount * 100.0 / model.TotalPredictions;
            }

            var totalWithResult = model.CorrectCount + model.WrongCount;
            if (totalWithResult > 0)
            {
                model.SuccessRate = model.CorrectCount * 100.0 / totalWithResult;
            }

            // predictions per day
            var grouped = history
                .GroupBy(h => h.CreatedAt.Date)
                .OrderBy(g => g.Key)
                .Select(g => new
                {
                    Date = g.Key.ToString("yyyy-MM-dd"),
                    Count = g.Count()
                })
                .ToList();

            model.PredictionsPerDayLabels = grouped.Select(g => g.Date).ToList();
            model.PredictionsPerDayCounts = grouped.Select(g => g.Count).ToList();

            return View(model);
        }
    }
}
