using Core.Entities;
using Core.Infrastructures;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAccountService
    {
        ClaimsIdentity Authenticate(string login, string password);

        Task<OperationDetails> RegisterAsync(string login, string password, string email);

        User GetInfo(string email);

    }
}
