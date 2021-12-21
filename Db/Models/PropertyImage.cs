using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExample.Db.Models
{
    [BsonIgnoreExtraElements]
    public class PropertyImage
    {
        [BsonId]
        public ObjectId Id { get; set; }
        
        /// <summary>
        /// IdProperty for the image
        /// </summary>
        public string IdProperty { get; set; }

        /// <summary>
        /// File of the image of the Property
        /// </summary>
        public string File { get; set; }
        /// <summary>
        /// Check if the image is Enabled
        /// </summary>
        public bool Enabled { get; set; }

    }
}
