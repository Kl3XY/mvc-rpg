using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using mvc_rpg.Data;
using mvc_rpg.Models;
using mvc_rpg.ViewModel;
using System;
using PagedList;
using System.Diagnostics;

namespace mvc_rpg.Controllers
{
    public class HomeController : Controller
    {
        private readonly RPGContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, RPGContext context)
        {
            _logger = logger;
            _context = context;
        }

        public string choose(string[] choose)
        {
            var rnd = new Random();
            return choose.Skip(rnd.Next(0, choose.Length)).First();
        }

        // GET: Players/Create
        public IActionResult Admin()
        {
            return View();
        }

        public IActionResult Index()
        {
            var vsScreen = new VersusScreen();
            var random = new Random();
            var enemies = _context.Enemies
                .Include(m => m.EnemyType)
                .Where(m => m.isAlive == true)
                .ToList();
            Enemy eList = null;
            if (enemies.Any())
            {
                eList = enemies
                .Skip(random.Next(enemies.Count()))
                .Take(1)
                .First();
            }
            var graves = _context.Graves
                .Include(m => m.Enemy)
                .Include(m => m.Player)
                .Take(20)
                .ToList();
                

            var players = _context.Players
                .Include(m => m.Items)
                .Where(m => m.isAlive == true)
                .ToList();
            Player pList = null;
            if (players.Any())
            {
                pList = players
                    .Skip(random.Next(players.Count()))
                    .Take(1)
                    .First();
            }
            
            vsScreen.Graves = graves.OrderByDescending(m => m.DateTime).ToList();
            if (eList == null) { ModelState.AddModelError(nameof(VersusScreen.Enemy), "All Enemies have been defeated"); return View(vsScreen); }
            if (pList == null) { ModelState.AddModelError(nameof(VersusScreen.Player), "All Players have been defeated"); return View(vsScreen); }
            vsScreen.Enemy = eList;
            vsScreen.Player = pList;
            vsScreen.PlayerKilledEnemies = graves
                .Where(m => m.PlayerID == vsScreen.Player.ID)
                .OrderByDescending(m => m.DateTime)
                .ToList();

            var rnd = new Random();
            var choose = rnd.Next(0, 2);
            if (choose == 0)
            {
                eList.isAlive = false;

                var amountOfItems = rnd.Next(1, 5);

                while(amountOfItems > 0)
                {
                    var newItem = new Item();
                    newItem.Name = this.choose(new string[] {"Stick", "Magazine", "Trophy", "Sword", "Golden Trophy", "Band Tape", "Mixtapes"});

                    var items = _context.ItemTypes
                    .Skip(random.Next(_context.ItemTypes.Count()))
                    .Take(1)
                    .First();

                    newItem.ItemTypeID = items.ID;

                    pList.Items.Add(newItem);

                    amountOfItems -= 1;
                }

                var Grave = new Grave()
                {
                    EnemyID = vsScreen.Enemy.ID,
                    PlayerID = vsScreen.Player.ID,
                    KilledBy = killedBy.Player,
                    DateTime = DateTime.Now
                };
                _context.Graves.Add(Grave);
            }
            else
            {
                pList.isAlive = false;
                var Grave = new Grave()
                {
                    EnemyID = vsScreen.Enemy.ID,
                    PlayerID = vsScreen.Player.ID,
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
        public IActionResult SearchPlayer(string searchTerm = "", int page = 0)
        {
            var players = _context.Players
                .Include(m => m.Items)
                .Where(m => m.Name.StartsWith(searchTerm))
                .Skip(10 * page)
                .Take(10)
                .ToList();

            if (players.Count() >= 10)
            {
                HttpContext.Session.SetInt32("canTurn", 1);
            } else
            {
                HttpContext.Session.SetInt32("canTurn", 0);
            }

            var newObj = new SearchPlayers();
            newObj.Players = players.ToList();
            newObj.Search = searchTerm;
            newObj.page = page;

            return View(newObj);
        }
        public IActionResult HallOfFame(string searchTerm = "", int page = 0)
        {
            var graves = _context.Graves
                .Include(m => m.Enemy)
                .Include(m => m.Player)
                .Where(m => m.KilledBy != killedBy.Enemy)
                .OrderBy(m => m.DateTime)
                .Take(100)
                .GroupBy(m => m.Player)
                .ToList();

            if (graves.Count() >= 100)
            {
                HttpContext.Session.SetInt32("canTurn", 1);
            }
            else
            {
                HttpContext.Session.SetInt32("canTurn", 0);
            }

            graves = graves.OrderByDescending(m => m.Count()).ToList();

            var HoE = new HallOfFame();
            HoE.Entries = graves;
            HoE.Search = searchTerm;
            HoE.page = page;

            return View(HoE);
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
