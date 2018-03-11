using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using UserService.Enum;

namespace UserService.Models
{
    public class User
    {
        [JsonIgnore]
        public int UserID;

        public string Username;
        public string FirstName;
        public string LastName;
        public string Email;
        public string Password;

        public Role Role;

        public User()
        {

        }
        
        /// <summary>
        /// Factory worker to create a new User out of query results
        /// </summary>
        /// <param name="reader"></param>
        public User(DbDataReader reader)
        {
            this.UserID = reader.GetInt32(0);
            this.Username = reader.GetString(1);
            this.Password = reader.GetString(2);
            this.Email = reader.GetString(3);
            this.FirstName = reader.GetString(4);
            this.LastName = reader.GetString(5);
            this.Role = (Role)reader.GetInt32(6);
        }
    }
}
