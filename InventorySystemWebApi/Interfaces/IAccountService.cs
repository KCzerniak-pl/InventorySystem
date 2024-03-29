﻿using InventorySystemWebApi.Models.Account;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystemWebApi.Interfaces
{
    public interface IAccountService
    {
        Task CreateAccount(CreateAccountDto dto);
        Task<string> LoginRequest([FromBody] LoginRequestDto dto);
    }
}
