using AutoMapper;
using Database;
using Database.Entities.Item;
using InventorySystemWebApi.Exceptions;
using InventorySystemWebApi.Intefaces;
using InventorySystemWebApi.Models;
using InventorySystemWebApi.Models.Item;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace InventorySystemWebApi.Services
{
    public class ItemService : IItemService
    {
        private readonly InventorySystemDbContext _dbContext;
        private readonly IMapper _mapper;

        public ItemService(InventorySystemDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PageWraper<ItemDto>> GetAll(PageQuery query)
        {
            // Get all items.
            var itemsAll = await _dbContext
                .Items
                .Include(i => i.Type)
                .Include(i => i.Group)
                .Include(i => i.Manufacturer)
                .Include(i => i.Seller)
                .Include(i => i.Location)
                .Where(c => query.SearchPhrase == null || c.Name.ToLower().Contains(query.SearchPhrase.ToLower(CultureInfo.CurrentCulture)))
                .ToListAsync();

            // Pagination.
            var items = itemsAll
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize);

            if (!items.Any())
            {
                // Custom exception (used middleware).
                throw new NotFoundException("Items not found.");
            }

            // Mapping to DTO.
            var itemsDto = _mapper.Map<IEnumerable<ItemDto>>(items);

            // Wrapping items.
            var result = new PageWraper<ItemDto>(itemsDto, itemsAll.Count());

            return result;
        }

        public async Task<ItemDto> GetByItem(int id)
        {
            // Get item about selected id.
            var item = await _dbContext
                .Items
                .Include(i => i.Type)
                .Include(i => i.Group)
                .Include(i => i.Manufacturer)
                .Include(i => i.Seller)
                .Include(i => i.Location)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item is null)
            {
                // Custom exception (used middleware).
                throw new NotFoundException("Item not found.");
            }

            // Mapping to DTO.
            var itemDto = _mapper.Map<ItemDto>(item);

            return itemDto;
        }

        public async Task<string> CreateItem(CreateItemDto dto)
        {
            // Mapping from DTO.
            var item = _mapper.Map<Item>(dto);

            // Add new item.
            _ = _dbContext.Items.Add(item);
            _ = await _dbContext.SaveChangesAsync();

            return $"/api/item/{item.Id}";
        }

        public async Task RemoveItem(int id)
        {
            // Get item about selected id.
            var item = await _dbContext
                .Items
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item is null)
            {
                // Custom exception (used middleware).
                throw new NotFoundException("Item not found.");
            }

            // Remove item.
            _ = _dbContext.Items.Remove(item);
            _ = await _dbContext.SaveChangesAsync();
        }
    }
}
