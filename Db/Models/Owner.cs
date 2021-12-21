using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExample.Db.Models
{
    [BsonIgnoreExtraElements]
    public class Owner
    {
        [BsonId]
        public ObjectId Id { get; set; }

        /// <summary>
        /// Name of the Owner
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Address of the Owner
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Photo of the Owner
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// Birthday of the Owner
        /// </summary>
        public DateTime Birthday { get; set; }

    }
}
