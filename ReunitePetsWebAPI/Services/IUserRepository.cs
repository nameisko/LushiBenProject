using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReunitePetsWebAPI.Services
{
    public interface IUserRepository
    {
        Task<bool> UserExists(string username);
        Task<AppUser> GetUserByUsername(string username);
        Task<bool> AuthenticateUser(AppUser user);
        Task<AppUser> CreateUser(AppUser user);
        Task<bool> Save();
    }
}
