using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using UrlShortener.Helpers;
using UrlShortener.Models;
using UrlShortener.MongoDB;

namespace UrlShortener.Repositories
{
    public class ShortenerRepository : IShortenerRepository
    {
        private readonly IMongoHelper _mongoHelper;
        private readonly IConfiguration _configuration;

        public ShortenerRepository(IMongoHelper mongoHelper, IConfiguration configuration)
        {
            _mongoHelper = mongoHelper;
            _configuration = configuration;
        }

        public async Task<UrlsModel> InsertOrGetExistingUrlAsync(string fullUrl)
        {
            var collectionName = MongoVariableNames.UrlsCollection;
            var document = await _mongoHelper.FindDocumentAsync<UrlsModel>(collectionName,
                nameof(UrlsModel.FullUrl), fullUrl);
            if (document != null)
                return document;

            var newUrl = new UrlsModel()
            {
                FullUrl = fullUrl,
                ShortUrl = await GetShortUrlCodeAsync()
            };
            await _mongoHelper.CreateDocumentAsync(collectionName, newUrl);
            return newUrl;
        }

        public async Task<List<UrlsModel>> GetAllUrlsAsync()
        {
            return await _mongoHelper.GetAllAsync<UrlsModel>(MongoVariableNames.UrlsCollection);
        }

        private async Task<string> GetShortUrlCodeAsync()
        {
            var shortUrlLength = int.Parse(_configuration["AppSettings:UrlCodeLength"]);

            // check if generated url code already exists. If so, generate new code until we get unique
            UrlsModel document = new UrlsModel();
            string urlCode = "";
            while (document != null)
            {
                urlCode = RandomStringGenerator.GetRandomString(shortUrlLength);
                document = await _mongoHelper.FindDocumentAsync<UrlsModel>(MongoVariableNames.UrlsCollection, nameof(UrlsModel.ShortUrl),
                    urlCode);
            }

            return urlCode;
        }
    }
}