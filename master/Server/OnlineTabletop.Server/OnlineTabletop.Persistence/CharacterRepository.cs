using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
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
        public IEnumerable<Character> GetCharactersByPlayerName(string playerName)
        {
            var characters = new HashSet<Character>();
            var database = client.GetServer().GetDatabase("tabletop");
            var playerColl = database.GetCollection(Util.Mongo.MongoUtilities.GetCollectionFromType(typeof(Player)));
            
            if (playerColl == null)
            {
                throw new Exception("No player collection in database");
            }

            var player = playerColl.AsQueryable<Player>().FirstOrDefault(x => x.AccountName == playerName);

            if (player != null && player.CharacterIds != null)
            {
                var collection = database.GetCollection(Util.Mongo.MongoUtilities.GetCollectionFromType(typeof(Character)));
                foreach (var characterId in player.CharacterIds)
                {
                    var character = collection.AsQueryable<Character>().FirstOrDefault(x => x._id == characterId);
                    if (character != null)
                    {
                        characters.Add(character);
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
            if (string.IsNullOrWhiteSpace(item.PlayerAccountName))
            {
                throw new ArgumentException("A player Id is required for the character.");
            }
            var database = client.GetServer().GetDatabase("tabletop");

            // Verify that the player exists in the database that we are going to be adding the character to.
            var playerColl = database.GetCollection(Util.Mongo.MongoUtilities.GetCollectionFromType(typeof(Player)));
            if (playerColl == null)
            {
                throw new Exception("No player collection currently in database.");

            }

            var player = playerColl.AsQueryable<Player>().FirstOrDefault(x => x.AccountName == item.PlayerAccountName);

            //var playerDoc = playerColl.FindOneById(new ObjectId(item.PlayerAccountName));
            if (player == null)
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

                if (player.CharacterIds == null)
                {
                    player.CharacterIds = new List<string>();
                }

                player.CharacterIds.Add(bsonId.ToString());

                var playerDoc = new BsonDocument();
                var bsonDocumentWriterSettings = new BsonDocumentWriterSettings();
                bsonDocumentWriterSettings.GuidRepresentation = GuidRepresentation.Standard;
                var bsonDocumentWriter = new BsonDocumentWriter(playerDoc, bsonDocumentWriterSettings);
                BsonSerializer.Serialize(bsonDocumentWriter, player);

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
