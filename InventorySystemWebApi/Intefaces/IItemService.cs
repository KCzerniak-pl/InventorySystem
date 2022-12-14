﻿using Database.Entities;
using InventorySystemWebApi.Models;

namespace InventorySystemWebApi.Intefaces
{
    public interface IItemService
    {
        Task<PageWraper<ItemDto>?> GetAll(ItemQuery query);
        Task<ItemDto?> GetByItem(int id);
        Task<int> CreateItem(CreateItemDto dto);
        Task RemoveItem(int id);
    }
}