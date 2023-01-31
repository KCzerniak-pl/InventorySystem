using AutoMapper;
using Database;
using InventorySystemWebApi.Exceptions;
using InventorySystemWebApi.Intefaces;
using InventorySystemWebApi.Models.Item;
using InventorySystemWebApi.Models;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace InventorySystemWebApi.Services
{
    public class GroupService : IGroupService
    {
        private readonly InventorySystemDbContext _dbContext;
        private readonly IMapper _mapper;

        public GroupService(InventorySystemDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PageWraper<GroupDto>> GetAll(PageQuery query)
        {
            // Get all groups.
            var groupsAll = await _dbContext
                .Groups
                .Where(c => query.SearchPhrase == null || c.Name.ToLower().Contains(query.SearchPhrase.ToLower(CultureInfo.CurrentCulture)))
                .ToListAsync();

            // Pagination.
            var groups = groupsAll
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize);

            if (!groups.Any())
            {
                // Custom exception (used middleware).
                throw new NotFoundException("Groups not found.");
            }

            // Mapping to DTO.
            var groupsDto = _mapper.Map<IEnumerable<GroupDto>>(groups);

            // Wrapping groups.
            var result = new PageWraper<GroupDto>(groupsDto, groupsAll.Count());

            return result;
        }

        public async Task<GroupDto> GetById(int id)
        {
            // Get group about selected id.
            var group = await _dbContext
                .Groups
                .FirstOrDefaultAsync(i => i.Id == id);

            if (group is null)
            {
                // Custom exception (used middleware).
                throw new NotFoundException("Group not found.");
            }

            // Mapping to DTO.
            var groupDto = _mapper.Map<GroupDto>(group);

            return groupDto;
        }
    }
}
