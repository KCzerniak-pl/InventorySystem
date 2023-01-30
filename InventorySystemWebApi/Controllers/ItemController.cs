using InventorySystemWebApi.Intefaces;
using InventorySystemWebApi.Models;
using InventorySystemWebApi.Models.Item;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystemWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet("/api/items")]
        public async Task<ActionResult<PageWraper<ItemDto>>> GetAll([FromQuery] PageQuery query)
        {
            // Get all items.
            var items = await _itemService.GetAll(query);

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetById([FromRoute] int id)
        {
            // Get item about selected id.
            var item = await _itemService.GetById(id);

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult> CreateItem([FromBody] CreateItemDto dto)
        {
            // Add new item.
            var itemUri = await _itemService.CreateItem(dto);

            return Created(itemUri, null);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveItem([FromRoute] int id)
        {
            // Remove item.
            await _itemService.RemoveItem(id);

            return NoContent();
        }
    }
}
