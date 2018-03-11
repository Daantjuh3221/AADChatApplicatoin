using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AADChatApplication.Models;
using RestSharp;

namespace AADChatApplication.UserAPI
{
    public class UserRepository : IUserRepository
    {
        ConsummingUserAPI api = new ConsummingUserAPI();

        public Task<IEnumerable<User>> FindAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> FindUserByIdAsync()
        {
            throw new NotImplementedException();
        }

        public async Task SaveUser(User item)
        {
            try
            {
                await api.Post<User>("/user", item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserVerification> VerifyUser(string Username, string Password)
        {
            try
            {
                return await api.Get<UserVerification>("/user/verifyuser?username="+Username+"&password=" + Password);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
