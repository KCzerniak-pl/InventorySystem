using InventorySystemWebApi.Models;
using InventorySystemWebApi.Models.Item;

namespace InventorySystemWebApi.Interfaces
{
    public interface IItemService
    {
        Task<PageWraper<ItemDto>> GetAll(PageQuery query);
        Task<ItemDto> GetById(int id);
        Task<string> CreateItem(CreateItemDto dto);
        Task RemoveItem(int id);
    }
}