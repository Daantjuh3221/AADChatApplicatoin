using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Common;
using MySql.Data.MySqlClient;
using System.Configuration;
using UserService.Models;
using UserService.Utility;

namespace UserService.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly string connStringSetting;

        /// <summary>
        /// Handles MySQL logic for users.
        /// MySQL Connections are automatically pooled, opening a new connection and letting the library handle pooling is safe
        /// See: https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/sql-server-connection-pooling Mysql uses ADO.NET as well.
        /// </summary>
        public UserRepository()
        {
            this.connStringSetting = ConfigurationManager.ConnectionStrings["UserDB"].ConnectionString;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<User> FindUserByID(int userId)
        {
            User user = null;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(this.connStringSetting))
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    await conn.OpenAsync();

                    cmd.CommandText = "SELECT * FROM user WHERE userid = @userid LIMIT 0,1";

                    cmd.Parameters.AddWithValue("@userid", userId);
                    cmd.Prepare();

                    using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                            user = new User(reader);
                    }
                }

            }
            catch (Exception e)
            {
                throw e;
            }

            return user;
        }

        /// <summary>
        /// Gets all users from db
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> FindAllUsers()
        {
            List<User> users = new List<User>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(this.connStringSetting))
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    await conn.OpenAsync();

                    cmd.CommandText = "SELECT * FROM user";

                    using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while(reader.Read()){
                            users.Add(new User(reader));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return users;
        }

        /// <summary>
        /// Creates a user
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<User> CreateUser(User item)
        {
            User user = null;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(this.connStringSetting))
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    await conn.OpenAsync();

                    cmd.CommandText = "INSERT INTO user "
                        + "(username, password, email, firstname, lastname) "
                        + "VALUES(@username, @password, @email, @firstname, @lastname)";

                    cmd.Parameters.AddWithValue("@username", item.Username);
                    cmd.Parameters.AddWithValue("@password", Password.Hash(item.Password));
                    cmd.Parameters.AddWithValue("@email", item.Email);
                    cmd.Parameters.AddWithValue("@firstname", item.FirstName);
                    cmd.Parameters.AddWithValue("@lastname", item.LastName);
                    cmd.Prepare();

                    using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                            user = new User(reader);
                    }
                }

            }
            catch (Exception e)
            {
                throw e;
            }

            return user;
        }

        /// <summary>
        /// Updates a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<bool> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes a user by id
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUserByID(int userid)
        {
            bool result = false;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(this.connStringSetting))
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    await conn.OpenAsync();

                    cmd.CommandText = "DELETE FROM user WHERE userid=@userid";

                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Prepare();

                    await cmd.ExecuteNonQueryAsync();
                    result = true;
                }

            }
            catch(Exception e)
            {
                throw e;
            }

            return result;
        }

        /// <summary>
        /// Deletes a user by id
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUserByUsername(string username)
        {
            bool result = false;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(this.connStringSetting))
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    await conn.OpenAsync();

                    cmd.CommandText = "DELETE FROM user WHERE username = @username LIMIT 0,1";

                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Prepare();

                    using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                            result = true;
                    }
                }

            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

        public async Task<UserVerification> VerifyUser(string username, string password)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(this.connStringSetting))
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    await conn.OpenAsync();

                    cmd.CommandText = "SELECT * FROM user WHERE username = @username LIMIT 0,1";

                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Prepare();

                    using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        UserVerification verification = new UserVerification();
                        verification.CredentialsAreCorrect = false;
                        if (reader.Read()){


                            string DbPassword = reader.GetString(2);
                            verification.UserID = reader.GetInt32(0);
                            verification.CredentialsAreCorrect = DbPassword == Password.Hash(password);
                            return verification;
                        }
                        return verification;
                    }
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
