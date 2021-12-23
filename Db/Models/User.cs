using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ApiExample.Db.Models
{
    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }

        /// <summary>
        /// Username for login
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Password for login
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Guid for user
        /// </summary>
        public Guid Guid { get; set; }
    }
}
