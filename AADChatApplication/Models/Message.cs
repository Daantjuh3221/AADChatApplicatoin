using System;
namespace AADChatApplication.Models
{
    public class Message
    {
        public int UserID { get; set; }

        public uint ServerId { get; set; }

        public DateTime TimeStamp { get; set; } = DateTime.Now;

        public String Content { get; set; }

        public Message()
        {

        }

        public Message(String Content, int UserID){
            this.Content = Content;
            this.UserID = UserID;
            this.ServerId = 1;
        }

    }
}
