using InventorySystemWebApi.Models;
using InventorySystemWebApi.Models.Item;

namespace InventorySystemWebApi.Intefaces
{
    public interface IItemService
    {
        Task<PageWraper<ItemDto>> GetAll(PageQuery query);
        Task<ItemDto> GetByItem(int id);
        Task<string> CreateItem(CreateItemDto dto);
        Task RemoveItem(int id);
    }
}