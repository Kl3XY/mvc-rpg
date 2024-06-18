using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvc_rpg.Data;
using mvc_rpg.Entities;
using System;
using System.Diagnostics;
using System.Drawing;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly ILogger<PlayersController> _logger;
        private readonly IMapper _mapper;
        private readonly RPGContext _context;

        public ItemsController(ILogger<PlayersController> logger, RPGContext context, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Read()
        {
            var items = await _context.Items
                .Include(m => m.ItemType)
                .ToListAsync();
            var mappedItems = _mapper.Map<List<mvc_rpg.Entities.Item>, List<Models.Item>>(items);

            return Ok(mappedItems);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ReadSingle([FromRoute]int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var item = await _context.Items
                .Include(m => m.ItemType)
                .FirstOrDefaultAsync(m => m.ID == id);
            var mappedItem = _mapper.Map<Models.Item>(item);
            
            if (item == null)
            {
                return NotFound("The given id didn't yield any item");
            }

            return Ok(mappedItem);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Models.Item Item)
        {
            await _context.Items.AddAsync(_mapper.Map<mvc_rpg.Entities.Item>(Item));
            _context.SaveChanges();

            return Ok(_context.Items.OrderByDescending(u => u.ID).FirstOrDefault());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(Models.Item item, [FromRoute]int id)
        {
            item.ID = id;

            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                _context.Update(_mapper.Map<mvc_rpg.Entities.Item>(item));
                _context.SaveChanges();
            }
            catch
            {
                return NotFound("The given id didn't yield any items");
            }
            
            return Ok(item);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 0)
            {
                return BadRequest("Invalid ID");
            }

            var item = await _context.Items.FirstOrDefaultAsync(m => m.ID == id);

            if (item == null)
            {
                return NotFound("The given id didn't yield any item");
            }

            _context.Items.Remove(item);
            _context.SaveChanges();
            
            return Ok("Entry Deleted Successfully");
        }
    }
}
