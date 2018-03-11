using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MessageService.Models;
using MessageService.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MessageService.Controllers
{
    [Produces("application/json")]
    [Route("api/message")]
    public class MessageController : Controller
    {
        private readonly IMessageRepository _messageRepository;

        public MessageController(IMessageRepository MessageRepository){
            this._messageRepository = MessageRepository;
        }

        //GET Endpoints
        [HttpGet]
        public async Task<IEnumerable<Message>> GetAllMessages()
        {
            return await _messageRepository.FindAllMessagesAsync();
        }

        [HttpGet("{ServerId}")]
        public async Task<IEnumerable<Message>> GetMessagesFromServerId(uint ServerId)
        {
            return await _messageRepository.FindMessagesByServerId(ServerId);
        }

        //POST & PUT Endpoint
        [HttpPost]
        public async void PostMessage([FromBody]Message incommingMessage)
        {
            await _messageRepository.SaveMessage(incommingMessage);
        }

        [HttpPut]
        public async void UpdateMessage([FromBody]Message updatedMessage)
        {
            await _messageRepository.UpdateMessage(updatedMessage);
        }

        //DELETE Endpoint
        [HttpDelete]
        public async void DeleteAllMessages()
        {
            await _messageRepository.RemoveAllMessages();
        }

        [HttpDelete("{ServerId}")]
        public async void DeleteMessagesOnServer(uint ServerId){
            await _messageRepository.RemoveMessagesOnServer(ServerId);
        }
    }
}
