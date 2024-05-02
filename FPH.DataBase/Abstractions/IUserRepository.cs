using FPH.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPH.DataBase.Abstractions
{
    public interface IUserRepository
    {
        Task<UserEntity> GetUserByIdAsync(string id);
        Task<UserEntity> GetUserBySurnameAsync(string surname);
        //Task<UserEntity> GetByEmailAsync(string email);
        Task<IEnumerable<UserEntity>> GetAllUsersAsync();
        Task UpdateAsync(UserEntity user);
    }
}
