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
    public class GravesController : ControllerBase
    {
        private readonly ILogger<PlayersController> _logger;
        private readonly IMapper _mapper;
        private readonly RPGContext _context;

        public GravesController(ILogger<PlayersController> logger, RPGContext context, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Read()
        {
            var graves = await _context.Graves.ToListAsync();
            var mappedGraves = _mapper.Map<List<mvc_rpg.Entities.Grave>, List<Models.Grave>>(graves);

            return Ok(mappedGraves);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ReadSingle([FromRoute]int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var grave = await _context.Graves.FirstOrDefaultAsync(m => m.ID == id);
            var mappedGrave = _mapper.Map<Models.Grave>(grave);
            
            if (grave == null)
            {
                return NotFound("The given id didn't yield any item");
            }

            return Ok(mappedGrave);
        }

        [HttpGet("HallOfFame")]
        public async Task<IActionResult> HallOfFame()
        {
            var graves = _context.Graves
                .Where(m => m.KilledBy != mvc_rpg.Entities.killedBy.Enemy)
                .OrderBy(m => m.DateTime)
                .Take(100)
                .ToList();

            var mappedGraves = _mapper.Map<List<mvc_rpg.Entities.Grave>, List<Models.Grave>>(graves);

            if (graves == null)
            {
                return NotFound("Hall of fame is empty");
            }

            var grouped = mappedGraves.GroupBy(m => m.PlayerID);

            return Ok(grouped);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Models.Grave grave)
        {
            await _context.Graves.AddAsync(_mapper.Map<mvc_rpg.Entities.Grave>(grave));
            _context.SaveChanges();

            return Ok(grave);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(Models.Grave grave, [FromRoute]int id)
        {
            grave.ID = id;

            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                _context.Update(_mapper.Map<mvc_rpg.Entities.Grave>(grave));
                _context.SaveChanges();
            }
            catch
            {
                return NotFound("The given id didn't yield any graves");
            }
            
            return Ok(grave);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 0)
            {
                return BadRequest("Invalid ID");
            }

            var grave = await _context.Graves.FirstOrDefaultAsync(m => m.ID == id);

            if (grave == null)
            {
                return NotFound("The given id didn't yield any item");
            }

            _context.Graves.Remove(grave);
            _context.SaveChanges();
            
            return Ok("Entry Deleted Successfully");
        }
    }
}
