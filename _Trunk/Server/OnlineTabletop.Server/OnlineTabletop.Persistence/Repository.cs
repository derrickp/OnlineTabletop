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
        }

        public T Get(string id)
        {
            var collection = client.GetServer().GetDatabase("tabletop").GetCollection(MongoUtilities.GetCollectionFromType(typeof(T)));

            var bsonDoc = collection.FindOneById(new ObjectId(id));
            if (bsonDoc != null)
            {
                RegisterMongoClassMaps();
                T returnVal = BsonSerializer.Deserialize<T>(bsonDoc);

                return returnVal;
            }
            return default(T);
        }

        public abstract void Add(T item);

        public void Clear()
        {
            var collection = client.GetServer().GetDatabase("tabletop").GetCollection(MongoUtilities.GetCollectionFromType(typeof(T)));
            collection.RemoveAll();
        }

        public bool Contains(T item)
        {
            var collection = client.GetServer().GetDatabase("tabletop").GetCollection(MongoUtilities.GetCollectionFromType(typeof(T)));
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
                var collection = client.GetServer().GetDatabase("tabletop").GetCollection(MongoUtilities.GetCollectionFromType(typeof(T)));
                return Convert.ToInt32(collection.Count());
            }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public bool Remove(T item)
        {
            var collection = client.GetServer().GetDatabase("tabletop").GetCollection(MongoUtilities.GetCollectionFromType(typeof(T)));
            
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var collection = client.GetServer().GetDatabase("tabletop").GetCollection(MongoUtilities.GetCollectionFromType(typeof(T)));
            RegisterMongoClassMaps();
            return (IEnumerator<T>)collection.FindAll().Select(bs => BsonSerializer.Deserialize<T>(bs));
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        internal void RegisterMongoClassMaps()
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
