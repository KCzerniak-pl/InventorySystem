using AutoMapper;
using Database;
using InventorySystemWebApi.Exceptions;
using InventorySystemWebApi.Interfaces;
using InventorySystemWebApi.Models;
using InventorySystemWebApi.Models.Item;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace InventorySystemWebApi.Services
{
    public class TypeService : ITypeService
    {
        private readonly InventorySystemDbContext _dbContext;
        private readonly IMapper _mapper;

        public TypeService(InventorySystemDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PageWrapper<TypeDto>> GetAll(PageQuery query)
        {
            // Get all types.
            var typesAll = await _dbContext
                .Types
                .AsNoTracking()
                .Where(c => string.IsNullOrEmpty(query.SearchPhrase) || c.Name.ToLower().Contains(query.SearchPhrase.ToLower(CultureInfo.CurrentCulture)))
                .ToListAsync();

            // Pagination.
            var types = typesAll
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize);

            if (!types.Any())
            {
                // Custom exception (to be caught by middleware).
                throw new NotFoundException("Types not found.");
            }

            // Map to DTO.
            var typesDto = _mapper.Map<IEnumerable<TypeDto>>(types);

            // Wrapping groups.
            var result = new PageWrapper<TypeDto>(typesDto, typesAll.Count());

            return result;
        }

        public async Task<TypeDto> GetById(int id)
        {
            // Get type about selected id.
            var type = await _dbContext
                .Types
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id);

            if (type is null)
            {
                // Custom exception (to be caught by middleware).
                throw new NotFoundException("Type not found.");
            }

            // Map to DTO.
            var typeDto = _mapper.Map<TypeDto>(type);

            return typeDto;
        }
    }
}
