using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mvc_rpg.Data;
using mvc_rpg.Models;

namespace mvc_rpg.Controllers
{
    public class EnemiesController : Controller
    {
        private readonly RPGContext _context;

        public EnemiesController(RPGContext context)
        {
            _context = context;
        }

        // GET: Enemies
        public async Task<IActionResult> Index()
        {
            var rPGContext = _context.Enemies.Include(e => e.EnemyType);
            return View(await rPGContext.ToListAsync());
        }

        // GET: Enemies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enemy = await _context.Enemies
                .Include(e => e.EnemyType)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (enemy == null)
            {
                return NotFound();
            }

            return View(enemy);
        }

        // GET: Enemies/Create
        public IActionResult Create()
        {
            ViewData["EnemyTypeID"] = new SelectList(_context.EnemyTypes, "ID", "Name");
            return View();
        }

        // POST: Enemies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,isAlive,EnemyTypeID")] Enemy enemy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enemy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EnemyTypeID"] = new SelectList(_context.EnemyTypes, "ID", "ID", enemy.EnemyTypeID);
            return View(enemy);
        }

        // GET: Enemies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enemy = await _context.Enemies.FindAsync(id);
            if (enemy == null)
            {
                return NotFound();
            }
            ViewData["EnemyTypeID"] = new SelectList(_context.EnemyTypes, "ID", "ID", enemy.EnemyTypeID);
            return View(enemy);
        }

        // POST: Enemies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,isAlive,EnemyTypeID")] Enemy enemy)
        {
            if (id != enemy.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enemy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnemyExists(enemy.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EnemyTypeID"] = new SelectList(_context.EnemyTypes, "ID", "ID", enemy.EnemyTypeID);
            return View(enemy);
        }

        // GET: Enemies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enemy = await _context.Enemies
                .Include(e => e.EnemyType)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (enemy == null)
            {
                return NotFound();
            }

            return View(enemy);
        }

        // POST: Enemies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enemy = await _context.Enemies.FindAsync(id);
            if (enemy != null)
            {
                _context.Enemies.Remove(enemy);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnemyExists(int id)
        {
            return _context.Enemies.Any(e => e.ID == id);
        }
    }
}
