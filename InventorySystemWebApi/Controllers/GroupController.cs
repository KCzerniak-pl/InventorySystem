using InventorySystemWebApi.Interfaces;
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
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet("/api/groups")]
        public async Task<ActionResult<PageWrapper<GroupDto>>> GetAll([FromQuery] PageQuery query)
        {
            // Get all groups.
            var groups = await _groupService.GetAll(query);

            return Ok(groups);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GroupDto>> GetById([FromRoute] int id)
        {
            // Get group about selected id.
            var group = await _groupService.GetById(id);

            return Ok(group);
        }
    }
}
