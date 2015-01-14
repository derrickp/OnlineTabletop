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
    public class Repository<T>: IRepository<T>
        where T:IEntity
    {

        MongoClient client { get; set; }

        public Repository(MongoClient client)
        {
            this.client = client;
        }

        public T Get(string id)
        {
            var typeParameter = typeof(T);
            var collectionName = typeParameter.Name.ToLowerInvariant();
            var collection = client.GetServer().GetDatabase("tabletop").GetCollection(collectionName);

            var temp = collection.FindOneById(new ObjectId(id));
            BsonClassMap.RegisterClassMap<T>(cm =>
            {
                cm.AutoMap();
                cm.IdMemberMap.SetRepresentation(BsonType.ObjectId);
            });

            T returnVal = BsonSerializer.Deserialize<T>(temp);

            throw new NotImplementedException();
        }

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get 
            {
                var typeParameter = typeof(T);
                var collectionName = typeParameter.Name.ToLowerInvariant();
                var collection = client.GetServer().GetDatabase("tabletop").GetCollection(collectionName);
                
                return Convert.ToInt32(collection.Count());
            }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
