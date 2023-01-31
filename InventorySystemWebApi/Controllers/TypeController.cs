using InventorySystemWebApi.Intefaces;
using InventorySystemWebApi.Models.Item;
using InventorySystemWebApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystemWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TypeController : ControllerBase
    {
        private readonly ITypeService _typeService;

        public TypeController(ITypeService typeService)
        {
            _typeService = typeService;
        }

        [HttpGet("/api/types")]
        public async Task<ActionResult<PageWraper<TypeDto>>> GetAll([FromQuery] PageQuery query)
        {
            // Get all types.
            var types = await _typeService.GetAll(query);

            return Ok(types);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TypeDto>> GetById([FromRoute] int id)
        {
            // Get type about selected id.
            var type = await _typeService.GetById(id);

            return Ok(type);
        }
    }
}
