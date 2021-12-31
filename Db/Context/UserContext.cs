using ApiExample.Db.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Security.Cryptography;
using System.Text;

namespace ApiExample.Db.Context
{
    public class UserContext
    {
        private IServiceContext _db { get; set; }
        public UserContext(IServiceContext db)
        {
            _db = db;
        }

        public bool IsValidUserCredentials(string userName, string password)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Username, userName);
            var user = _db.User.Find(filter).ToList();

            if (user == null)
                return false;

            return user[0].Password.Contains(hashPassword(password, user[0].Guid.ToString()));
        }

        public ObjectId SaveUser(string userName, string password)
        {
            var guid = Guid.NewGuid();
            var user = new User
            {
                Id = ObjectId.GenerateNewId(),
                Username = userName,
                Password = hashPassword(password, guid.ToString()),
                Guid = guid
            };
            
            _db.User.InsertOne(user);
            return user.Id;
        }











        /// <summary>
        /// Funcion que hace hash con el guid y la password aleatoria
        /// </summary>
        /// <param name="enteredPass"></param>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        private static string hashPassword(string enteredPass, string userGuid)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(enteredPass + userGuid);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += string.Format("{0:x2}", x);//transformo de hash a string
            }
            return hashString;
        }
    }
}
