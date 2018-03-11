using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using UserService.Models;
using UserService.Repository;

namespace UserService.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    public class UserController : Controller
    {
        //private readonly IUserRepository _userRepository;
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Creates new instance of the DB stuff
        /// </summary>
        public UserController(IUserRepository UserRepository)
        {
            this._userRepository = UserRepository;
        }

        // GET api/values
        [HttpGet]
        public async Task<List<User>> GetUsers()
        {
            return await this._userRepository.FindAllUsers();
        }

        [HttpGet("{userId}")]
        public async Task<User> GetUser(uint userId)
        {
            //return new User();
            return await this._userRepository.FindUserByID((int)userId);
        }

        [HttpPost]
        public async Task<User> PostUser([FromBody]User body)
        {
            return await this._userRepository.CreateUser(body);
        }

        [HttpGet]
        [Route("verifyuser")]
        public async Task<UserVerification> VerifyUser(string username, string password)
        {
            return await this._userRepository.VerifyUser(username,password);
        }

        //DELETE Endpoint
        [HttpDelete("{userId}")]
        public async void DeleteUserByID(int userId)
        {
            await this._userRepository.DeleteUserByID(userId);
        }

        [HttpDelete("{username}")]
        public async void DeleteUserByUsername(string username)
        {
            await this._userRepository.DeleteUserByUsername(username);
        }
    }
}
