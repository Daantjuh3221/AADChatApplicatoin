using System;
using MessageService.Enum;

namespace MessageService.Models
{
    public class User
    {
        public String FirstName
        {
            get;
            set;
        }

        public String LastName
        {
            get;
            set;
        }

        public String Email
        {
            get;
            set;
        }

        public String Password
        {
            get;
            set;
        }

        public Role Role
        {
            get;
            set;
        }
        public User()
        {
        }
    }
}
