using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Models;

namespace UserService.Repository
{
    public interface IUserRepository
    {
        Task<User> FindUserByID(int userId);
        Task<List<User>> FindAllUsers();

        Task<User> CreateUser(User item);

        Task<bool> UpdateUser(User user);

        Task<bool> DeleteUserByID(int userId);
        Task<bool> DeleteUserByUsername(string username);
        Task<UserVerification> VerifyUser(string username, string password);
    }
}
