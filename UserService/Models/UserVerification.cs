using System;
namespace UserService.Models
{
    public class UserVerification
    {
        public bool CredentialsAreCorrect
        {
            get;
            set;
        }

        public int UserID
        {
            get;
            set;
        }
        public UserVerification()
        {
        }
    }
}
