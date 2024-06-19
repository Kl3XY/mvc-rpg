using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvc_rpg.Data;
using mvc_rpg.Entities;
using System;
using System.Drawing;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnemyEnemyTypeController : ControllerBase
    {
        private readonly ILogger<EnemiesController> _logger;
        private readonly IMapper _mapper;
        private readonly RPGContext _context;

        public EnemyEnemyTypeController(ILogger<EnemiesController> logger, RPGContext context, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Read()
        {
            var enemies = await _context.Enemies
                .Include(m => m.EnemyType)
                .ToListAsync();
            var mappedEnemy = _mapper.Map<List<mvc_rpg.Entities.Enemy>, List<Models.EnemyEnemyType>>(enemies);

            return Ok(mappedEnemy);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ReadSingle([FromRoute]int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var enemy = await _context.Enemies
                .Include(m => m.EnemyType)
                .FirstOrDefaultAsync(m => m.ID == id);
            var mappedEnemy = _mapper.Map<Models.EnemyEnemyType>(enemy);
            
            if (enemy == null)
            {
                return NotFound("The given id didn't yield any enemies");
            }

            return Ok(mappedEnemy);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Models.Enemy enemy)
        {
            await _context.Enemies.AddAsync(_mapper.Map<mvc_rpg.Entities.Enemy>(enemy));
            _context.SaveChanges();

            return Ok(enemy);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(Models.Enemy enemy, [FromRoute]int id)
        {
            enemy.ID = id;

            if (enemy.EnemyTypeID <= 0 || id <= 0)
            {
                return BadRequest();
            }

            try
            {
                _context.Update(_mapper.Map<mvc_rpg.Entities.Enemy>(enemy));
                _context.SaveChanges();
            }
            catch
            {
                return NotFound("The given id didn't yield any enemy");
            }
            
            return Ok(enemy);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 0)
            {
                return BadRequest("Invalid ID");
            }

            var enemy = _context.Enemies.FirstOrDefault(m => m.ID == id);

            if (enemy == null)
            {
                return NotFound("The given id didn't yield any item");
            }

            _context.Enemies.Remove(enemy);
            _context.SaveChanges();
            
            return Ok("Entry Deleted Successfully");
        }
    }
}
