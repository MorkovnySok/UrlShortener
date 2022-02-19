using System.Collections.Generic;
using System.Threading.Tasks;

namespace UrlShortener.MongoDB
{
    public interface IMongoHelper
    {
        public Task<List<T>> GetAllAsync<T>(string collectionName);
        public Task<T> FindDocumentAsync<T>(string collectionName, string fieldToSearchBy, string searchValue);
        public Task CreateDocumentAsync<T>(string collectionName, T document);
    }
}