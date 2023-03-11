using InventorySystemWebApi.Models;
using InventorySystemWebApi.Models.Item;

namespace InventorySystemWebApi.Interfaces
{
    public interface IGroupService
    {
        Task<PageWrapper<GroupDto>> GetAll(PageQuery query);
        Task<GroupDto> GetById(int id);
    }
}
