using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AADChatApplication.Models;

namespace AADChatApplication.UserAPI
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> FindAllUsersAsync();

        Task<IEnumerable<User>> FindUserByIdAsync();

        Task SaveUser(User item);

        Task<UserVerification> VerifyUser(string Username, string Password);
    }
}
