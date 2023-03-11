using InventorySystemWebApi.Models.Item;
using InventorySystemWebApi.Models;

namespace InventorySystemWebApi.Interfaces
{
    public interface ITypeService
    {
        Task<PageWraper<TypeDto>> GetAll(PageQuery query);
        Task<TypeDto> GetById(int id);
    }
}
