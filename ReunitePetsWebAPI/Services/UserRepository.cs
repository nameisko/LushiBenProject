using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReunitePetsWebAPI.Services
{
    public class UserRepository : IUserRepository
    {
        private ReunitePetsDbContext _context;

        public UserRepository(ReunitePetsDbContext context)
        {
            _context = context;
        }

        public async Task<AppUser> GetUserByUsername(string username)
        {
            IQueryable<AppUser> result;
            
            result = _context.AppUsers.Where(u => u.Username == username);

            return await result.FirstOrDefaultAsync();
        }

        public async Task<bool> AuthenticateUser(AppUser user)
        {
            if (await UserExists(user.Username))
            {
                AppUser userFromDb = await _context.AppUsers.Where(u => u.Username == user.Username).FirstOrDefaultAsync();
                if (userFromDb.Username == user.Username && userFromDb.Password == user.Password)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public async Task<AppUser> CreateUser(AppUser user)
        {
            await _context.AppUsers.AddAsync(user);
            _context.Entry(user).GetDatabaseValues();

            return user;
        }

        public async Task<bool> UserExists(string username)
        {
            return await _context.AppUsers.AnyAsync<AppUser>(u => u.Username == username);
        }

        public async Task<bool> Save()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
