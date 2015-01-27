using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTabletop.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using System.Reflection;
using MongoDB.Bson.Serialization;

namespace OnlineTabletop.Persistence
{
    public abstract class Repository<T>: IRepository<T>
        where T:IEntity
    {

        public MongoClient client { get; set; }

        public Repository(MongoClient client)
        {
            this.client = client;
            RegisterMongoClassMaps();
        }

        public T Get(string id)
        {
            var collection = client.GetServer().GetDatabase("tabletop").GetCollection(Util.Mongo.MongoUtilities.GetCollectionFromType(typeof(T)));

            var bsonDoc = collection.FindOneById(new ObjectId(id));
            if (bsonDoc != null)
            {
                T returnVal = BsonSerializer.Deserialize<T>(bsonDoc);

                return returnVal;
            }
            return default(T);
        }

        public virtual void Add(T item)
        {
            var database = client.GetServer().GetDatabase("tabletop");

            var bsonDoc = new BsonDocument();
            var bsonDocumentWriterSettings = new BsonDocumentWriterSettings();
            bsonDocumentWriterSettings.GuidRepresentation = GuidRepresentation.Standard;
            var bsonDocumentWriter = new BsonDocumentWriter(bsonDoc, bsonDocumentWriterSettings);
            BsonSerializer.Serialize(bsonDocumentWriter, item);
            var collection = database.GetCollection(Util.Mongo.MongoUtilities.GetCollectionFromType(item.GetType()));
            var saveOptions = new MongoInsertOptions();
            saveOptions.WriteConcern = WriteConcern.Acknowledged;
            var succeeded = collection.Save(bsonDoc, saveOptions);
            if (!succeeded.Ok)
            {
                throw new Exception(succeeded.LastErrorMessage);
            }
        }

        public void Clear()
        {
            var collection = client.GetServer().GetDatabase("tabletop").GetCollection(Util.Mongo.MongoUtilities.GetCollectionFromType(typeof(T)));
            collection.RemoveAll();
        }

        public bool Contains(T item)
        {
            // If there is no id, we can't check whether the item exists.
            if (string.IsNullOrWhiteSpace(item._id)) return false;

            var collection = client.GetServer().GetDatabase("tabletop").GetCollection(Util.Mongo.MongoUtilities.GetCollectionFromType(typeof(T)));
            var bsonDoc = collection.FindOneById(new ObjectId(item._id));
            return bsonDoc != null;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get 
            {
                var collection = client.GetServer().GetDatabase("tabletop").GetCollection(Util.Mongo.MongoUtilities.GetCollectionFromType(typeof(T)));
                return Convert.ToInt32(collection.Count());
            }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public bool Remove(T item)
        {
            var collection = client.GetServer().GetDatabase("tabletop").GetCollection(Util.Mongo.MongoUtilities.GetCollectionFromType(typeof(T)));
            
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var collection = client.GetServer().GetDatabase("tabletop").GetCollection(Util.Mongo.MongoUtilities.GetCollectionFromType(typeof(T)));
            return (IEnumerator<T>)collection.FindAll().Select(bs => BsonSerializer.Deserialize<T>(bs));
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        internal void RegisterMongoClassMaps()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(T)))
            {
                // Register the way to deserialize the class using the Mongo Deserializer
                // Setting the representation of the ObjectId to become a string.
                BsonClassMap.RegisterClassMap<T>(cm =>
                {
                    cm.AutoMap();
                    cm.IdMemberMap.SetRepresentation(BsonType.ObjectId);
                });
            }
        }
    }
}
