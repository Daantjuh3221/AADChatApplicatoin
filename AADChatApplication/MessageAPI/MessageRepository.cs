using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AADChatApplication.Models;
using RestSharp;

namespace AADChatApplication.MessageAPI
{
    public class MessageRepository : IMessageRepository
    {
        ConsummingMessageAPI api = new ConsummingMessageAPI();
        
        public async Task<IEnumerable<Message>> FindAllMessagesAsync()
        {
            try
            {
                return await api.Get<IEnumerable<Message>>("/message");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Message>> FindMessagesByServerId(uint ServerId)
        {
            try
            {
                return await api.Get<IEnumerable<Message>>("/message/" + ServerId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task SaveMessage(Message item)
        {
            try
            {
                await api.Post<Message>("/message", item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
