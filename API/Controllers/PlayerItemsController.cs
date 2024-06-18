using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvc_rpg.Data;
using mvc_rpg.Entities;
using System;
using System.Drawing;
using System.Numerics;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerItemsController : ControllerBase
    {
        private readonly ILogger<PlayersController> _logger;
        private readonly IMapper _mapper;
        private readonly RPGContext _context;

        public PlayerItemsController(ILogger<PlayersController> logger, RPGContext context, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Read()
        {
            var players = await _context.Players
                .Include(m => m.Items)
                    .ThenInclude(i => i.ItemType)
                .ToListAsync();
            var mappedPlayer = _mapper.Map<List<mvc_rpg.Entities.Player>, List<Models.PlayerItem>>(players);

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

            var player = await _context.Players
                .Include(m => m.Items)
                    .ThenInclude(i => i.ItemType)
                .FirstOrDefaultAsync(m => m.ID == id);
            var mappedPlayer = _mapper.Map<Models.PlayerItem>(player);
            
            if (player == null)
            {
                return NotFound("The given id didn't yield any player");
            }

            return Ok(mappedPlayer);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute]int id, int itemid)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                var player = await _context.Players
                    .Include(m => m.Items)
                .FirstOrDefaultAsync(m => m.ID == id);
                
                var item = await _context.Items
                .FirstOrDefaultAsync(m => m.ID == itemid);

                player.Items.Add(item);

                _context.SaveChanges();
            }
            catch
            {
                return NotFound("The given id didn't yield any players");
            }
            
            return Ok();
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
