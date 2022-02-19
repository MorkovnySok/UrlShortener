using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace UrlShortener.MongoDB
{
    public class MongoHelper : IMongoHelper
    {
        private readonly IMongoDatabase _db;

        public MongoHelper(IConfiguration config)
        {
            var client = new MongoClient();
            _db = client.GetDatabase(MongoVariableNames.DatabaseName);
        }

        public async Task<List<T>> GetAllAsync<T>(string collectionName)
        {
            var collection = _db.GetCollection<T>(collectionName);
            var data = collection.Find(new BsonDocument()).ToList();
            return data;
        }

        public async Task<T> FindDocumentAsync<T>(string collectionName, string fieldToSearchBy, string searchValue)
        {
            var collection = _db.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq(fieldToSearchBy, searchValue);
            var document = await collection.FindAsync(filter);
            return document.FirstOrDefault();
        }

        public async Task CreateDocumentAsync<T>(string collectionName, T document)
        {
            var collection = _db.GetCollection<T>(collectionName);
            await collection.InsertOneAsync(document);
        }
    }
}