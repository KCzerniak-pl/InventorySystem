using InventorySystemWebApi.Models.Account;
using InventorySystemWebApi.Models.Item;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystemWebApi.Intefaces
{
    public interface IAccountService
    {
        Task CreateAccount(CreateAccountDto dto);
        Task<string> LoginRequest([FromBody] LoginRequestDto dto);
    }
}
