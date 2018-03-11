using MessageService.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MessageService.Repository
{
    public class MessageContext
    {
        private readonly IMongoDatabase _database = null;

        public MessageContext(IOptions<Settings> Settings)
        {
            var client = new MongoClient(Settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(Settings.Value.Database);
        }

        public IMongoCollection<Message> Messages
        {
            get
            {
                return _database.GetCollection<Message>("Message");
            }
        }
    }
}
