using System.Linq;
using System.Threading.Tasks;
using Football.Web.Data;
using Football.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Football.Web.Services;


namespace Football.Web.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IFootballStatsGrpcClient _statsClient;


        public TeamsController(ApplicationDbContext context, IFootballStatsGrpcClient statsClient)
        {
            _context = context;
            _statsClient = statsClient;
        }

        // GET: Teams
        public async Task<IActionResult> Index()
        {
            var teams = _context.Teams
                .Include(t => t.League);

            return View(await teams.ToListAsync());
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .Include(t => t.League)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }
        // GET: Teams/Form/5
        public async Task<IActionResult> Form(int id, int lastN = 5)
        {
            var team = await _context.Teams
                .Include(t => t.League)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (team == null)
            {
                return NotFound();
            }

            var formResponse = await _statsClient.GetTeamFormAsync(id, lastN);
            var lastMatchesResponse = await _statsClient.GetLastNMatchesAsync(id, lastN);

            var vm = new TeamFormViewModel
            {
                Team = team,
                LastN = lastN,
                Form = formResponse,
                LastMatches = lastMatchesResponse
            };

            return View(vm);
        }
        // GET: Teams/HeadToHead
        public async Task<IActionResult> HeadToHead()
        {
            var teams = await _context.Teams
                .OrderBy(t => t.Name)
                .ToListAsync();

            ViewBag.Teams = new SelectList(teams, "Id", "Name");

            var model = new HeadToHeadViewModel
            {
                LastN = 5
            };

            return View(model);
        }

        // POST: Teams/HeadToHead
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HeadToHead(HeadToHeadViewModel model)
        {
            var teams = await _context.Teams
                .OrderBy(t => t.Name)
                .ToListAsync();

            ViewBag.Teams = new SelectList(teams, "Id", "Name");

            if (model.HomeTeamId == 0 || model.AwayTeamId == 0 || model.HomeTeamId == model.AwayTeamId)
            {
                ModelState.AddModelError(string.Empty, "Te rog selectează două echipe diferite.");
                return View(model);
            }

            var homeTeam = teams.First(t => t.Id == model.HomeTeamId);
            var awayTeam = teams.First(t => t.Id == model.AwayTeamId);

            var stats = await _statsClient.GetHeadToHeadAsync(
                model.HomeTeamId,
                model.AwayTeamId,
                model.LastN
            );

            model.HomeTeam = homeTeam;
            model.AwayTeam = awayTeam;
            model.Stats = stats;

            return View(model);
        }


        // GET: Teams/Create
        public IActionResult Create()
        {
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "Id", "Name");
            return View();
        }

        // POST: Teams/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ShortName,LeagueId")] Team team)
        {
            if (ModelState.IsValid)
            {
                _context.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["LeagueId"] = new SelectList(_context.Leagues, "Id", "Name", team.LeagueId);
            return View(team);
        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            ViewData["LeagueId"] = new SelectList(_context.Leagues, "Id", "Name", team.LeagueId);
            return View(team);
        }

        // POST: Teams/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ShortName,LeagueId")] Team team)
        {
            if (id != team.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["LeagueId"] = new SelectList(_context.Leagues, "Id", "Name", team.LeagueId);
            return View(team);
        }

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .Include(t => t.League)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team != null)
            {
                _context.Teams.Remove(team);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.Id == id);
        }
    }
}
