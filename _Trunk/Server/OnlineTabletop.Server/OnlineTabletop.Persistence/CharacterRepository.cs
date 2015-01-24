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
    public class CharacterRepository : Repository<Character>, ICharacterRepository<Character>
    {
        public IEnumerable<Character> GetCharactersByPlayerId(string playerId)
        {
            var characters = new HashSet<Character>();
            var database = client.GetServer().GetDatabase("tabletop");
            var playerCollection = database.GetCollection(MongoUtilities.GetCollectionFromType(typeof(Player)));
            
            if (playerCollection == null)
            {
                throw new Exception("No player collection in database");
            }
            
            var playerDoc = playerCollection.FindOneById(new ObjectId(playerId));
            if (playerDoc == null)
            {
                throw new ArgumentException("No player found with Id given.");
            }
            
            var characterIdsVal = playerDoc.GetValue("characterIds", BsonValue.Create(new BsonArray()));
            if (!characterIdsVal.IsBsonNull && characterIdsVal.IsBsonArray)
            {
                var characterIdsArray = characterIdsVal.AsBsonArray;
                if (characterIdsArray.Count > 0)
                {
                    var characterIds = characterIdsArray.ToList().Select(x => x.ToString());
                    var collection = database.GetCollection(MongoUtilities.GetCollectionFromType(typeof(Character)));
                    if (collection == null)
                    {
                        throw new Exception("No characters in the database.");
                    }
                    foreach (var characterId in characterIds)
                    {
                        var characterDoc = collection.FindOneById(new ObjectId(characterId));
                    }
                }
            }
            return characters;
        }

        public new System.Collections.IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Adds the item to the collection. Modifies the item to add an id that can be used to retrieve the item afterwards.
        /// </summary>
        /// <param name="item">The item to add to the collection.</param>
        public override void Add(Character item)
        {
            if (!string.IsNullOrWhiteSpace(item._id) && this.Contains<Character>(item))
            {
                throw new ArgumentException("Item already exists in collection.");
            }
            if (string.IsNullOrWhiteSpace(item.PlayerId))
            {
                throw new ArgumentException("A player Id is required for the character.");
            }
            var database = client.GetServer().GetDatabase("tabletop");

            // Verify that the player exists in the database that we are going to be adding the character to.
            var playerColl = database.GetCollection(MongoUtilities.GetCollectionFromType(typeof(Player)));
            if (playerColl == null)
            {
                throw new Exception("No player collection currently in database.");

            }

            var playerDoc = playerColl.FindOneById(new ObjectId(item.PlayerId));
            if (playerDoc == null)
            {
                throw new Exception("No player found with Id specified.");
            }

            try
            {
                // Create the id here because then I can add it to the character.
                var bsonId = ObjectId.GenerateNewId(DateTime.Now);
                item._id = bsonId.ToString();
                base.Add(item);

                // character has been successfully saved. 
                // now need to update the character ids of this player.
                // code to update the player that got this character
                var characterIdsVal = playerDoc.GetValue("characterIds", BsonValue.Create(new BsonArray()));

                if (characterIdsVal.IsBsonNull)
                {
                    characterIdsVal = new BsonArray();
                }

                if (!characterIdsVal.IsBsonArray)
                {
                    characterIdsVal = new BsonArray();
                }

                var characterIdsArray = characterIdsVal.AsBsonArray;
                var bsonIdVal = new BsonString(bsonId.ToString());
                characterIdsArray.Add(bsonIdVal);
                playerDoc.Set("characterIds", characterIdsArray);
                var succeeded = playerColl.Save(playerDoc);
                if (!succeeded.Ok)
                {
                    throw new Exception("Character saved, player unsuccessfully updated. " + succeeded.LastErrorMessage);
                }

            }
            catch (Exception ex)
            {
                // One of the saves failed. Pass it up.
                throw ex;
            }
        }

        public CharacterRepository(MongoClient client)
            : base(client)
        {

        }
    }
}
