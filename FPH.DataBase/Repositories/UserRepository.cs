using FPH.Data.Entities;
using FPH.DataBase.Abstractions;
using FPH.DataBase.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPH.DataBase.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserEntity> GetUserByIdAsync(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<UserEntity>> GetUserBySurnameAsync(string surname)
        {
            return await _context.Users.Where(u => u.LastName == surname).ToListAsync();
        }
        //public async Task<UserEntity> GetByEmailAsync (string email)
        //{
        //    return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        //}
        public async Task<IEnumerable<UserEntity>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task UpdateAsync (UserEntity user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
