using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExample.Db.Models
{
    [BsonIgnoreExtraElements]
    public class Property
    {       
        [BsonId]
        public ObjectId Id { get; set; }

        /// <summary>
        /// Name of the Property
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Address of the Property
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Price of the Property without decimals
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Code for internal reference of the Property
        /// </summary>
        public string CodeInternal { get; set; }
        
        /// <summary>
        /// Year of the Property
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// IdOwner of the Property
        /// </summary>
        public string IdOwner { get; set; }

    }
}
