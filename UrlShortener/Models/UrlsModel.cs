using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UrlShortener.Models
{
    public class UrlsModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string ShortUrl { get; set; }
        public string FullUrl { get; set; }
    }
}
