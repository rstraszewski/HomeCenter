using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using HomeCenter.AuthenticationDomain.Model.DomainLayer.Enums;
using HomeCenter.CommonDomain;

namespace HomeCenter.AuthenticationDomain
{
    public class User : IAggregateRoot
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string Roles { get; set; }
        public string RoomAccess { get; set; }
        private readonly RNGCryptoServiceProvider _cryptoServiceProvider;
        private const int SALT_SIZE = 24;

        public User()
        {
            
        }


        public User(string passwordText, string username)
        {
            _cryptoServiceProvider = new RNGCryptoServiceProvider();
            Salt = GetSaltString();
            var hashedPassword = passwordText + Salt;
            PasswordHash = GetPasswordHashAndSalt(hashedPassword);
            Roles = "User";
            Username = username;
        }

        public bool IsPasswordMatch(string password)
        {
            var hashedPassword = password + Salt;
            return PasswordHash == GetPasswordHashAndSalt(hashedPassword);
        }

        private byte[] GetBytes(string str)
        {
            var bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
     
        private string GetString(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        private string GetPasswordHashAndSalt(string message)
        {
            SHA256 sha = new SHA256CryptoServiceProvider();
            var dataBytes = GetBytes(message);
            var resultBytes = sha.ComputeHash(dataBytes);
            return Convert.ToBase64String(resultBytes);
        }

        private string GetSaltString()
        {
            var saltBytes = new byte[SALT_SIZE];
            _cryptoServiceProvider.GetNonZeroBytes(saltBytes);
            var saltString = GetString(saltBytes);
            return saltString;
        }

        
    }
}
