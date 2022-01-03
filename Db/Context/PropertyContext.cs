using ApiExample.Core.Extensions;
using ApiExample.Db.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApiExample.Db.Context
{
    public class PropertyContext
    {
        private IServiceContext _db { get; set; }
        public PropertyContext(IServiceContext db)
        {
            _db = db;
        }

        public Property FindPropertyById(string id)
        {
            var filter = Builders<Property>.Filter.Eq(cr => cr.Id, ObjectId.Parse(id));
            return _db.Property.Find(filter).FirstOrDefault();
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

        public bool UpdateOwner(string id, Owner owner)
        {
            var filter = Builders<Owner>.Filter.Eq(cr => cr.Id, ObjectId.Parse(id));
            var update = Builders<Owner>.Update.Set(c => c.Name, owner.Name)
                .SetIfValue(c => c.Birthday, owner.Birthday)
                .SetIfValue(c => c.Photo, owner.Photo)
                .SetIfValue(c => c.Address, owner.Address);

            var response = _db.Owner.UpdateOne(filter, update);

            return response.IsAcknowledged;
        }

        public bool UpdateProperty(string id, Property property)
        {
            var filter = Builders<Property>.Filter.Eq(cr => cr.Id, ObjectId.Parse(id));
            var update = Builders<Property>.Update.Set(c => c.Name, property.Name)
                .SetIfValue(c => c.Price, property.Price)
                .SetIfValue(c => c.IdOwner, property.IdOwner)
                .SetIfValue(c => c.CodeInternal, property.CodeInternal)
                .SetIfValue(c => c.Year, property.Year)
                .SetIfValue(c => c.Address, property.Address);

            var response = _db.Property.UpdateOne(filter, update);

            return response.IsAcknowledged;
        }

        public List<Property> FindAllProperties()
        {
            var filter = Builders<Property>.Filter.Empty;
            return _db.Property.Find(filter).ToList();
        }

        public List<Property> FindPropertiesByFilter(string f)
        {
            var r = new Regex(f, RegexOptions.IgnoreCase);
            var filter = Builders<Property>.Filter.Or(
                    Builders<Property>.Filter.Regex(c => c.Name, r),
                    Builders<Property>.Filter.Regex(c => c.Address, r),
                    Builders<Property>.Filter.Regex(c => c.CodeInternal, r)
                );

            return _db.Property.Find(filter).ToList();
        }
    }
}
