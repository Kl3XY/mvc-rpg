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
    public class ItemTypesController : ControllerBase
    {
        private readonly ILogger<PlayersController> _logger;
        private readonly IMapper _mapper;
        private readonly RPGContext _context;

        public ItemTypesController(ILogger<PlayersController> logger, RPGContext context, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Read()
        {
            var itemTypes = await _context.ItemTypes.ToListAsync();
            var mappedItemTypes = _mapper.Map<List<mvc_rpg.Entities.ItemType>, List<Models.ItemType>>(itemTypes);

            return Ok(mappedItemTypes);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ReadSingle([FromRoute]int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var itemType = await _context.ItemTypes.FirstOrDefaultAsync(m => m.ID == id);
            var mappedItemType = _mapper.Map<Models.ItemType>(itemType);
            
            if (itemType == null)
            {
                return NotFound("The given id didn't yield any item");
            }

            return Ok(mappedItemType);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Models.ItemType ItemType)
        {
            await _context.ItemTypes.AddAsync(_mapper.Map<mvc_rpg.Entities.ItemType>(ItemType));
            _context.SaveChanges();

            return Ok(ItemType);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(Models.ItemType itemType, [FromRoute]int id)
        {
            itemType.ID = id;

            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                _context.Update(_mapper.Map<mvc_rpg.Entities.ItemType>(itemType));
                _context.SaveChanges();
            }
            catch
            {
                return NotFound("The given id didn't yield any itemType");
            }


            
            return Ok(itemType);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 0)
            {
                return BadRequest("Invalid ID");
            }

            var itemType = await _context.ItemTypes.FirstOrDefaultAsync(m => m.ID == id);

            if (itemType == null)
            {
                return NotFound("The given id didn't yield any item");
            }

            _context.ItemTypes.Remove(itemType);
            _context.SaveChanges();
            
            return Ok("Entry Deleted Successfully");
        }
    }
}
