using System;
namespace AADChatApplication.Models
{
    public class UserVerification
    {
        public bool CredentialsAreCorrect
        {
            get;
            set;
        } = false;

        public int UserID
        {
            get;
            set;
        }
    }
}
