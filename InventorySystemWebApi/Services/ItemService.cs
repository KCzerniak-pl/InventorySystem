using AutoMapper;
using Database;
using Database.Entities;
using InventorySystemWebApi.Exceptions;
using InventorySystemWebApi.Intefaces;
using InventorySystemWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;

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

        public async Task<IEnumerable<ItemDto>?> GetAll()
        {
            // Get all items.
            var items = await _dbContext
                .Items
                .Include(i => i.Type)
                .Include(i => i.Group)
                .Include(i => i.Manufacturer)
                .Include(i => i.Seller)
                .Include(i => i.Location)
                .ToListAsync();

            if (!items.Any())
            {
                // Custom exception (used middleware).
                throw new NotFoundException("Items not found.");
            }

            // Mapping to DTO.
            var itemsDto = _mapper.Map<IEnumerable<ItemDto>>(items);

            return itemsDto;
        }

        public async Task<ItemDto?> GetByItem(int id)
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

        public async Task<int> CreateItem(CreateItemDto dto)
        {
            // Mapping from DTO.
            var item = _mapper.Map<Item>(dto);

            // Add new item.
            _ = _dbContext.Items.Add(item);
            _ = await _dbContext.SaveChangesAsync();

            return item.Id;
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
