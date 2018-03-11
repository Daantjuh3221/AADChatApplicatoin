using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace UserService.Utility
{
    public static class Password
    {
        public static string Hash(string password)
        {
            byte[] passwordBytes = System.Text.Encoding.Unicode.GetBytes(password);
            SHA256 mysha = SHA256.Create();
            byte[] hashed = mysha.ComputeHash(passwordBytes);

            return Convert.ToBase64String(hashed);
        }
    }
}
