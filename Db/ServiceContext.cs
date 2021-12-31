using ApiExample.Db.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExample.Db
{
    public interface IServiceContext
    {
        IMongoCollection<Owner> Owner { get; }
        IMongoCollection<Property> Property { get; }
        IMongoCollection<PropertyTrace> PropertyTrace { get; }
        IMongoCollection<PropertyImage> PropertyImage { get; }
        IMongoCollection<User> User { get; }

    }
    public class ServiceContext : IServiceContext
    {
        private IMongoDatabase _db { get; }
        public ServiceContext(DbSettings config,
            IOptions<AppSettings> settings)
        {
            var mongoClient = new MongoClient(config.Connection);
            _db = mongoClient.GetDatabase(settings.Value.ServicesDb.Db);
        }

        public IMongoCollection<Owner> Owner => _db.GetCollection<Owner>("Owner");
        public IMongoCollection<Property> Property => _db.GetCollection<Property>("Property");
        public IMongoCollection<PropertyTrace> PropertyTrace => _db.GetCollection<PropertyTrace>("PropertyTrace");
        public IMongoCollection<PropertyImage> PropertyImage => _db.GetCollection<PropertyImage>("PropertyImage");
        public IMongoCollection<User> User => _db.GetCollection<User>("User");
    }
}
