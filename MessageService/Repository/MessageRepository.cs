using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MessageService.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MessageService.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly MessageContext _context = null;

        public MessageRepository(IOptions<Settings> settings)
        {
            _context = new MessageContext(settings);
        }

        public async Task SaveMessage(Message item)
        {
            try
            {
                await _context.Messages.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Message>> FindAllMessagesAsync()
        {
            try
            {
                return await _context.Messages.Find(_ => true).ToListAsync();
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
                return await _context.Messages.Find(a => a.ServerId.Equals(ServerId)).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> RemoveAllMessages()
        {
            try
            {
                DeleteResult actionResult
                = await _context.Messages.DeleteManyAsync(new BsonDocument());

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> RemoveMessagesOnServer(uint ServerId)
        {
            try
            {
                DeleteResult actionResult
                = await _context.Messages.DeleteManyAsync(
                    Builders<Message>.Filter.Eq("ServerId", ServerId));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateMessage(Message item)
        {
            try
            {
                ReplaceOneResult actionResult
                = await _context.Messages
                                .ReplaceOneAsync(n => n._id.Equals(item._id)
                                            , item
                                            , new UpdateOptions { IsUpsert = true });
                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
