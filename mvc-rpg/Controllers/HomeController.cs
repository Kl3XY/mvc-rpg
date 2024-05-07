using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using mvc_rpg.Data;
using mvc_rpg.Models;
using mvc_rpg.ViewModel;
using System;
using System.Diagnostics;

namespace mvc_rpg.Controllers
{
    public class HomeController : Controller
    {
        private readonly RPGContext _context;
        private string searchTerm = "";
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, RPGContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var vsScreen = new VersusScreen();
            var random = new Random();
            var enemies = _context.Enemies
                .Include(m => m.EnemyType)
                .Where(m => m.isAlive == true);
            var eList = enemies
                .Skip(random.Next(enemies.Count()))
                .Take(1)
                .ToList();

            var graves = _context.Graves
                .Include(m => m.Enemy)
                .Include(m => m.Player)
                .Take(20);
                

            var players = _context.Players
                .Include(m => m.Items)
                .Where(m => m.isAlive == true);
            var pList = players
                .Skip(random.Next(players.Count()))
                .Take(1);
            vsScreen.Graves = graves.OrderByDescending(m => m.DateTime).ToList();
            if (eList.Count == 0) { ModelState.AddModelError(nameof(VersusScreen.Enemy), "All Enemies have been defeated"); return View(vsScreen); }
            if (pList.ToList().Count == 0) { ModelState.AddModelError(nameof(VersusScreen.Player), "All Players have been defeated"); return View(vsScreen); }
            vsScreen.Enemy = enemies.First();
            vsScreen.Player = players.First();
            vsScreen.PlayerKilledEnemies = graves
                .Where(m => m.PlayerID == vsScreen.Player.ID)
                .OrderByDescending(m => m.DateTime)
                .ToList();

            var rnd = new Random();
            if (rnd.Next(0, 2) == 0)
            {
                enemies.First().isAlive = false;

                var amountOfItems = rnd.Next(1, 5);

                while(amountOfItems > 0)
                {
                    var items = _context.Items
                    .Skip(random.Next(_context.Items.Count()))
                    .Take(1)
                    .First();

                    pList.First().Items.Add(items);

                    amountOfItems -= 1;
                }

                var Grave = new Grave()
                {
                    EnemyID = vsScreen.Enemy.ID,
                    PlayerID = vsScreen.Enemy.ID,
                    KilledBy = killedBy.Player,
                    DateTime = DateTime.Now
                };
                _context.Graves.Add(Grave);
            }
            else
            {
                players.First().isAlive = false;
                var Grave = new Grave()
                {
                    EnemyID = vsScreen.Enemy.ID,
                    PlayerID = vsScreen.Enemy.ID,
                    KilledBy = killedBy.Enemy,
                    DateTime = DateTime.Now
                };
                _context.Graves.Add(Grave);
            }
            _context.SaveChanges();


            return View(vsScreen);
        }

        public async Task<IActionResult> Inventory()
        {
            var players = await _context.Players
                .Include(m => m.Items)
                .FirstOrDefaultAsync(m => m.ID == HttpContext.Session.GetInt32("user_ID"));

            return View(players);
        }

        [HttpGet]
        public IActionResult SearchPlayer()
        {
            var players = _context.Players
                .Include(m => m.Items)
                .Where(m => m.Name.Contains(searchTerm));

            var newObj = new SearchPlayers();
            newObj.Players = players.ToList();
            newObj.Search = searchTerm;

            return View(newObj);
        }
        [HttpPost]
        public IActionResult SearchPlayer(SearchPlayers searchPlayers)
        {
            searchTerm = searchPlayers.Search;

            return Redirect("/Home/SearchPlayer");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user_ID");
            return Redirect("/Players/Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
