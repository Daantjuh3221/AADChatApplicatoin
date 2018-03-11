using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AADChatApplication.MessageAPI;
using AADChatApplication.Models;
using AADChatApplication.UserAPI;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AADChatApplication.Controllers
{
    public class ChatController : Controller
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;

        public ChatController(IMessageRepository MessageRepository, IUserRepository UserRepository){
            this._userRepository = UserRepository;
            this._messageRepository = MessageRepository;
        }

        public async Task<IActionResult> Index()
        {
            int UserID = Convert.ToInt32(Request.Cookies["UserID"]);
            IEnumerable<Message> messageList = await _messageRepository.FindMessagesByServerId(1);
            IEnumerable<User> userList = await _userRepository.FindAllUsersAsync();
            return View(MessageViewModel.messageViewsList(UserID, messageList, userList.ToList()));
        }

        [HttpPost]
        public async Task<IActionResult> Index(string content)
        {
            int UserID = Convert.ToInt32(Request.Cookies["UserID"]);

            Message newMessage = new Message(content, UserID);
            await _messageRepository.SaveMessage(newMessage);
            
            IEnumerable<Message> messageList = await _messageRepository.FindMessagesByServerId(1);
            IEnumerable<User> userList = await _userRepository.FindAllUsersAsync();
            return View(MessageViewModel.messageViewsList(UserID, messageList, userList.ToList()));
        }
    }
}
