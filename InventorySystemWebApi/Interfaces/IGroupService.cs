using InventorySystemWebApi.Models.Item;
using InventorySystemWebApi.Models;

namespace InventorySystemWebApi.Interfaces
{
    public interface IGroupService
    {
        Task<PageWraper<GroupDto>> GetAll(PageQuery query);
        Task<GroupDto> GetById(int id);
    }
}
