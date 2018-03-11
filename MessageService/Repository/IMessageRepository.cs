using System.Collections.Generic;
using System.Threading.Tasks;
using MessageService.Models;

namespace MessageService.Repository
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> FindAllMessagesAsync();

        Task<IEnumerable<Message>> FindMessagesByServerId(uint ServerId);

        Task SaveMessage(Message item);

        Task<bool> UpdateMessage(Message message);

        Task<bool> RemoveMessagesOnServer(uint ServerId);

        Task<bool> RemoveAllMessages();
    }
}
