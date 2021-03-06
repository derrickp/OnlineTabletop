﻿using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using OnlineTabletop.Models;

namespace OnlineTabletop.Accounts
{
    public class AccountManager: IAccountManager<Account>
    {
        internal const string pepper = "_onlinetabletop_password_pepper_spiderman";

        private MongoClient _client;

        public AccountManager(MongoClient client)
        {
            _client = client;
            if (!BsonClassMap.IsClassMapRegistered(typeof(Account)))
            {
                // Register the way to deserialize the class using the Mongo Deserializer
                // Setting the representation of the ObjectId to become a string.
                BsonClassMap.RegisterClassMap<Account>(cm =>
                {
                    cm.AutoMap();

                    cm.IdMemberMap.SetRepresentation(BsonType.ObjectId);
                });
            }
        }

        public Account FindAccountByEmail(string email)
        {
            var collection = _client.GetServer().GetDatabase("tabletop").GetCollection(Util.Mongo.MongoUtilities.GetCollectionFromType(typeof(Account)));
            var account = collection.AsQueryable<Account>().FirstOrDefault(x => x.email == email);
            return account;
        }

        public Account FindAccountByName(string name)
        {
            var collection = _client.GetServer().GetDatabase("tabletop").GetCollection(Util.Mongo.MongoUtilities.GetCollectionFromType(typeof(Account)));
            var account = collection.AsQueryable<Account>().FirstOrDefault(x => x.name == name);
            return account;
        }

        public bool VerifyLogin(string name, string password)
        {
            var account = FindAccountByName(name);
            var salt = account.salt;
            var hash = HashPassword(password, salt);
            
            return account.hash == hash;
        }

        /// <summary>
        /// Adds a new acc
        /// </summary>
        /// <param name="registerBindingModel"></param>
        /// <returns></returns>
        public Account Add(RegisterBindingModel registerBindingModel)
        {
            var collection = _client.GetServer().GetDatabase("tabletop").GetCollection(Util.Mongo.MongoUtilities.GetCollectionFromType(typeof(Account)));
            if (collection == null)
            {
                throw new Exception("No account collection in database.");
            }
            
            var hashDict = HashPassword(registerBindingModel.Password);

            if (!hashDict.ContainsKey("hash") || !hashDict.ContainsKey("salt"))
            {
                throw new Exception("Could not hash password properly.");
            }

            var id = ObjectId.GenerateNewId(DateTime.Now).ToString();
            
            var account = new Account()
            {
                _id = id,
                name = registerBindingModel.Name,
                email = registerBindingModel.Email,
                salt = hashDict["salt"],
                hash = hashDict["hash"]
            };
            
            var bsonDoc = new BsonDocument();
            var bsonDocumentWriterSettings = new BsonDocumentWriterSettings();
            bsonDocumentWriterSettings.GuidRepresentation = GuidRepresentation.Standard;
            var bsonDocumentWriter = new BsonDocumentWriter(bsonDoc, bsonDocumentWriterSettings);
            BsonSerializer.Serialize(bsonDocumentWriter, account);
            var saveOptions = new MongoInsertOptions();
            saveOptions.WriteConcern = WriteConcern.Acknowledged;
            var succeeded = collection.Save(bsonDoc, saveOptions);
            if (!succeeded.Ok)
            {
                throw new Exception(succeeded.LastErrorMessage);
            }

            var playerId = ObjectId.GenerateNewId(DateTime.Now).ToString();

            var player = new Player()
            {
                _id = playerId,
                AccountName = account.name,
                Email = account.email,
                JoinDate = DateTime.Now
            };

            var playerCollection = _client.GetServer().GetDatabase("tabletop").GetCollection(Util.Mongo.MongoUtilities.GetCollectionFromType(player.GetType()));
            bsonDoc = new BsonDocument();
            bsonDocumentWriter = new BsonDocumentWriter(bsonDoc, bsonDocumentWriterSettings);
            BsonSerializer.Serialize(bsonDocumentWriter, player);
            succeeded = playerCollection.Save(bsonDoc, saveOptions);
            if (!succeeded.Ok)
            {
                throw new Exception(succeeded.LastErrorMessage);
            }

            return account;
        }

        private Dictionary<string,string> HashPassword(string password)
        {
            var dict = new Dictionary<string, string>();
            
            var salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            var hash = HashPassword(password, salt);

            dict["salt"] = salt;
            dict["hash"] = hash;

            return dict;
        }

        private string HashPassword(string password, string salt)
        {
            string hash = BCrypt.Net.BCrypt.HashPassword(password + pepper, salt);

            return hash;
        }
    }
}
