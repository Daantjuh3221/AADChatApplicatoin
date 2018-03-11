using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace MessageService.Models
{
    public class Message
    {
        [BsonId]
        [JsonIgnore]
        public ObjectId _id { get; set; }

        public int UserID { get; set; }

        public uint ServerId { get; set; }

        public DateTime TimeStamp { get; set; } = DateTime.Now;

        public String Content {get; set;}

    }
}
