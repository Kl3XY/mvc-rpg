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
    public class EnemiesController : ControllerBase
    {
        private readonly ILogger<EnemiesController> _logger;
        private readonly IMapper _mapper;
        private readonly RPGContext _context;

        public EnemiesController(ILogger<EnemiesController> logger, RPGContext context, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Read()
        {
            var enemies = await _context.Enemies.ToListAsync();
            var mappedEnemy = _mapper.Map<List<mvc_rpg.Entities.Enemy>, List<Models.Enemy>>(enemies);

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

            var enemy = await _context.Enemies.FirstOrDefaultAsync(m => m.ID == id);
            var mappedEnemy = _mapper.Map<Models.Enemy>(enemy);
            
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

            _context.Update(_mapper.Map<mvc_rpg.Entities.Enemy>(enemy));
            _context.SaveChanges();
            
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

            _context.Enemies.Remove(enemy);
            _context.SaveChanges();
            
            return Ok("Entry Deleted Successfully");
        }
    }
}
