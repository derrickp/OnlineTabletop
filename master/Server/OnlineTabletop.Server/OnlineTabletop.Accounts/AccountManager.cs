using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;

namespace OnlineTabletop.Accounts
{
    public class AccountManager: IAccountManager<DbAccount>
    {
        private MongoClient _client;

        public AccountManager(MongoClient client)
        {
            _client = client;
            if (!BsonClassMap.IsClassMapRegistered(typeof(DbAccount)))
            {
                // Register the way to deserialize the class using the Mongo Deserializer
                // Setting the representation of the ObjectId to become a string.
                BsonClassMap.RegisterClassMap<DbAccount>(cm =>
                {
                    cm.AutoMap();
                    cm.IdMemberMap.SetRepresentation(BsonType.ObjectId);
                });
            }
        }

        public DbAccount FindAccountByEmail(string email)
        {
            var collection = _client.GetServer().GetDatabase("tabletop").GetCollection(Util.Mongo.MongoUtilities.GetCollectionFromType(typeof(DbAccount)));
            var dbAccount = collection.AsQueryable<DbAccount>().FirstOrDefault(x => x.email == email);
            return dbAccount;
        }
    }
}
