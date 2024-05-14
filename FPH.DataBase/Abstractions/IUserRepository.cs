using FPH.Data.Entities;

namespace FPH.DataBase.Abstractions
{
    public interface IUserRepository
    {
        Task<UserEntity> GetUserByIdAsync(string id);
        Task<IEnumerable<UserEntity>> GetUserBySurnameAsync(string surname);
        //Task<UserEntity> GetByEmailAsync(string email);
        Task<IEnumerable<UserEntity>> GetAllUsersAsync();
        Task UpdateAsync(UserEntity user);
    }
}
