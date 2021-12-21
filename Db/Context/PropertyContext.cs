using ApiExample.Db.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExample.Db.Context
{
    public class PropertyContext
    {
        private ServiceContext _db { get; set; }
        public PropertyContext(ServiceContext db)
        {
            _db = db;
        }

        public ObjectId SaveOwner(Owner owner)
        {
            owner.Id = ObjectId.GenerateNewId();
            _db.Owner.InsertOne(owner);
            return owner.Id;
        }

        public ObjectId SaveProperty(Property property)
        {
            property.Id = ObjectId.GenerateNewId();
            _db.Property.InsertOne(property);
            return property.Id;
        }
    }
}
