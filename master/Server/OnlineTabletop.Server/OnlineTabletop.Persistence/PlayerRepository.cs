﻿using MongoDB.Bson;
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
            var collection = client.GetServer().GetDatabase("tabletop").GetCollection(Util.Mongo.MongoUtilities.GetCollectionFromType(typeof(Player)));
            var player = collection.AsQueryable<Player>().FirstOrDefault(x => x.Email == email);
            return player;
        }

        public Player GetByAccountName(string accountName)
        {
            var collection = client.GetServer().GetDatabase("tabletop").GetCollection(Util.Mongo.MongoUtilities.GetCollectionFromType(typeof(Player)));
            var player = collection.AsQueryable<Player>().FirstOrDefault(x => x.AccountName == accountName);
            return player;
        }

        public override void Add(Player item)
        {
            if (!string.IsNullOrWhiteSpace(item._id) && this.Contains<Player>(item))
            {
                throw new ArgumentException("Item already exists in collection.");
            }
            
            // Create the id here instead of the base. Create it here because I may need it later.
            var bsonId = ObjectId.GenerateNewId(DateTime.Now);
            item._id = bsonId.ToString();
            base.Add(item);
        }

        public PlayerRepository(MongoClient client) : base (client)
        {

        }
    }
}