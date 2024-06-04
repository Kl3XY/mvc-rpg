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
    public class EnemyTypeController : ControllerBase
    {
        private readonly ILogger<PlayerController> _logger;
        private readonly IMapper _mapper;
        private readonly RPGContext _context;

        public EnemyTypeController(ILogger<PlayerController> logger, RPGContext context, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Read()
        {
            var enemyTypes = await _context.EnemyTypes.ToListAsync();
            var mappedEnemyTypes = _mapper.Map<List<mvc_rpg.Entities.EnemyType>, List<Models.EnemyType>>(enemyTypes);

            return Ok(mappedEnemyTypes);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ReadSingle([FromRoute]int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var enemyType = await _context.EnemyTypes.FirstOrDefaultAsync(m => m.ID == id);
            var mappedEnemyType = _mapper.Map<Models.EnemyType>(enemyType);
            
            if (enemyType == null)
            {
                return NotFound("The given id didn't yield any item");
            }

            return Ok(mappedEnemyType);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Models.EnemyType enemyType)
        {
            await _context.EnemyTypes.AddAsync(_mapper.Map<mvc_rpg.Entities.EnemyType>(enemyType));
            _context.SaveChanges();

            return Ok(enemyType);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(Models.EnemyType enemyType, [FromRoute]int id)
        {
            enemyType.ID = id;

            if (id <= 0)
            {
                return BadRequest();
            }

            _context.Update(_mapper.Map<mvc_rpg.Entities.EnemyType>(enemyType));
            _context.SaveChanges();
            
            return Ok(enemyType);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 0)
            {
                return BadRequest("Invalid ID");
            }

            var enemyType = await _context.EnemyTypes.FirstOrDefaultAsync(m => m.ID == id);

            if (enemyType == null)
            {
                return NotFound("The given id didn't yield any item");
            }

            _context.EnemyTypes.Remove(enemyType);
            _context.SaveChanges();
            
            return Ok("Entry Deleted Successfully");
        }
    }
}
