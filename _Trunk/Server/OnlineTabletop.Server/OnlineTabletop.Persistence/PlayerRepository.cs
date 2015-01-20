using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using OnlineTabletop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTabletop.Persistence
{
    public class PlayerRepository : Repository<Player>, IPlayerRepository<Player>
    {
        public Player GetByEmail(string email)
        {
            var collection = client.GetServer().GetDatabase("tabletop").GetCollection(MongoUtilities.GetCollectionFromType(typeof(Player)));
            var player = collection.AsQueryable<Player>().FirstOrDefault(x => x.email == email);
            return player;
        }

        public override void Add(Player item)
        {
            if (!string.IsNullOrWhiteSpace(item._id) && this.Contains<Player>(item))
            {
                throw new ArgumentException("Item already exists in collection.");
            }
            
            var bsonDoc = new BsonDocument();
            var bsonDocumentWriterSettings = new BsonDocumentWriterSettings();
            bsonDocumentWriterSettings.GuidRepresentation = GuidRepresentation.Standard;
            var bsonDocumentWriter = new BsonDocumentWriter(bsonDoc, bsonDocumentWriterSettings);
            BsonSerializer.Serialize(bsonDocumentWriter, item);
            bsonDoc.Set("_id", new ObjectId());
            var collection = client.GetServer().GetDatabase("tabletop").GetCollection(MongoUtilities.GetCollectionFromType(typeof(Player)));
            var saveOptions = new MongoInsertOptions();
            saveOptions.WriteConcern = WriteConcern.Acknowledged;
            var succeeded = collection.Save(bsonDoc, saveOptions);
            if (!succeeded.Ok)
            {
                throw new Exception(succeeded.LastErrorMessage);
            }
        }

        public PlayerRepository(MongoClient client) : base (client)
        {

        }
    }
}