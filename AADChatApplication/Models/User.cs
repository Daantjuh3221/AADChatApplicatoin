using System;
using AADChatApplication.Enum;

namespace AADChatApplication.Models
{
    public class User
    {
        public int UserID;

        public string Username;
        public string FirstName;
        public string LastName;
        public string Email;
        public string Password;

        public Role Role;

        public User(String Username, String FirstName, String LastName, String Email, String Password)
        {
            this.Username = Username;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Password = Password;
            this.Role = Role.Teacher;
        }
        public User()
        {

        }
    }
}
