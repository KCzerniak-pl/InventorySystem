using InventorySystemWebApi.Models;
using InventorySystemWebApi.Models.Item;

namespace InventorySystemWebApi.Interfaces
{
    public interface ITypeService
    {
        Task<PageWrapper<TypeDto>> GetAll(PageQuery query);
        Task<TypeDto> GetById(int id);
    }
}
