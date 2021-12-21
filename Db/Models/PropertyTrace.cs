using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExample.Db.Models
{
    [BsonIgnoreExtraElements]
    public class PropertyTrace
    {
        [BsonId]
        public ObjectId Id { get; set; }

        /// <summary>
        /// Name of the Property
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Date of sale of the Property
        /// </summary>
        public DateTime DateSale { get; set; }
        /// <summary>
        /// Value of the Property without decimals
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// Taxes of the Property without decimals
        /// </summary>
        public int Tax { get; set; }
        /// <summary>
        /// IdProperty for the trace
        /// </summary>
        public string IdProperty { get; set; }

    }
}
