using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AADChatApplication.Models;

namespace AADChatApplication.MessageAPI
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> FindAllMessagesAsync();

        Task<IEnumerable<Message>> FindMessagesByServerId(uint ServerId);

        Task SaveMessage(Message item);
    }
}
