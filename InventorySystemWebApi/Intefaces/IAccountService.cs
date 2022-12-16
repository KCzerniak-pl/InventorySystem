using InventorySystemWebApi.Models.Account;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystemWebApi.Intefaces
{
    public interface IAccountService
    {
        Task<string> LoginRequest([FromBody] LoginRequestDto dto);
    }
}
