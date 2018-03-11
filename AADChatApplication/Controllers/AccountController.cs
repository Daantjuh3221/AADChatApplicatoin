using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AADChatApplication.Models;
using AADChatApplication.UserAPI;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AADChatApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository UserRepository)
        {
            this._userRepository = UserRepository;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            UserVerification userVerification = await _userRepository.VerifyUser(username, password);
            if (userVerification != null && userVerification.CredentialsAreCorrect)
            {
                Response.Cookies.Append("UserID", userVerification.UserID.ToString());
                return RedirectToAction("Index", "Chat");
            }
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password, string firstname, string lastname, string email)
        {
            await _userRepository.SaveUser(new User(username,firstname,lastname,email,password));
            return RedirectToAction("Login", "Account");
        }
    }
}
