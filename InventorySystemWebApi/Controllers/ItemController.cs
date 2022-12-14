using InventorySystemWebApi.Intefaces;
using InventorySystemWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystemWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet("/api/items")]
        public async Task<ActionResult<PageWraper<ItemDto>>> GetAll([FromQuery] ItemQuery query)
        {
            // Get all items.
            var items = await _itemService.GetAll(query);

            return StatusCode(StatusCodes.Status200OK, items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetById([FromRoute] int id)
        {
            // Get item about selected id.
            var item = await _itemService.GetByItem(id);

            return StatusCode(StatusCodes.Status200OK, item);
        }

        [HttpPost]
        public async Task<ActionResult> CreateItem([FromBody] CreateItemDto dto)
        {
            // Add new item.
            var itemId = await _itemService.CreateItem(dto);

            return StatusCode(StatusCodes.Status201Created, itemId);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveItem([FromRoute] int id)
        {
            // Remove item.
            await _itemService.RemoveItem(id);

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
