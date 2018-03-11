using System;
using System.Collections.Generic;
using AADChatApplication.UserAPI;

namespace AADChatApplication.Models
{
    public class MessageViewModel
    {
        public string From { get; set; }

        public DateTime TimeStamp { get; set; }

        public String Content { get; set; }

        public String FromUsername { get; set; }

        public static List<MessageViewModel> messageViewsList(int UserID, IEnumerable<Message> MessageList, List<User> UserList){
            List<MessageViewModel> messageViewModels = new List<MessageViewModel>();
            foreach (var message in MessageList)
            {
                MessageViewModel messageView = new MessageViewModel();
                messageView.From = UserID == message.UserID ? "self" : "other";
                messageView.FromUsername = UserList.Find(a => a.UserID.Equals(UserID)).Username;
                messageView.TimeStamp = message.TimeStamp;
                messageView.Content = message.Content;
                messageViewModels.Add(messageView);
            }
            return messageViewModels;
        }
    }
}
