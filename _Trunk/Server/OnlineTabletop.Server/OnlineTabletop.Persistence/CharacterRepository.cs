using MongoDB.Bson;
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
    class CharacterRepository : Repository<Character>, ICharacterRepository<Character>
    {
        public IEnumerable<Character> GetByPlayerId(string playerId)
        {
            var collection = client.GetServer().GetDatabase("tabletop").GetCollection(MongoUtilities.GetCollectionFromType(typeof(Player)));
            var playerDoc = collection.FindOneById(new ObjectId(playerId));

            var characterIds = playerDoc.GetValue("characterIds", BsonValue.Create(new List<string>()));
            return new HashSet<Character>();
        }

        public new System.Collections.IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public override void Add(Character item)
        {
            throw new NotImplementedException();
        }

        public CharacterRepository(MongoClient client)
            : base(client)
        {

        }
    }
}
