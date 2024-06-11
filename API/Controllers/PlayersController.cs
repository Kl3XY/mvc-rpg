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
    public class PlayersController : ControllerBase
    {
        private readonly ILogger<PlayersController> _logger;
        private readonly IMapper _mapper;
        private readonly RPGContext _context;

        public PlayersController(ILogger<PlayersController> logger, RPGContext context, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Read()
        {
            var players = await _context.Players.ToListAsync();
            var mappedPlayer = _mapper.Map<List<mvc_rpg.Entities.Player>, List<Models.Player>>(players);

            return Ok(mappedPlayer);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ReadSingle([FromRoute]int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var player = await _context.Players.FirstOrDefaultAsync(m => m.ID == id);
            var mappedPlayer = _mapper.Map<Models.Player>(player);
            
            if (player == null)
            {
                return NotFound("The given id didn't yield any player");
            }

            return Ok(mappedPlayer);
        }

        [HttpGet]
        [Route("Search/{name}")]
        public async Task<IActionResult> SearchPlayer([FromRoute] string name)
        {
            if (name == String.Empty)
            {
                return BadRequest();
            }

            var player = _context.Players.Where(m => m.Name.StartsWith(name)).ToList();
            var mappedPlayer = _mapper.Map<List<mvc_rpg.Entities.Player>, List<Models.Player>>(player);

            if (player == null)
            {
                return NotFound("The given search didn't yield any player");
            }

            return Ok(mappedPlayer);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Models.Player player)
        {
            await _context.Players.AddAsync(_mapper.Map<mvc_rpg.Entities.Player>(player));
            _context.SaveChanges();

            return Ok(player);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(Models.Player player, [FromRoute]int id)
        {
            player.ID = id;

            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                _context.Update(_mapper.Map<mvc_rpg.Entities.Player>(player));
                _context.SaveChanges();
            }
            catch
            {
                return NotFound("The given id didn't yield any players");
            }
            
            return Ok(player);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 0)
            {
                return BadRequest("Invalid ID");
            }

            var player = _context.Players.FirstOrDefault(m => m.ID == id);

            if (player == null)
            {
                return NotFound("The given id didn't yield any item");
            }

            _context.Players.Remove(player);
            _context.SaveChanges();
            
            return Ok("Entry Deleted Successfully");
        }
    }
}
