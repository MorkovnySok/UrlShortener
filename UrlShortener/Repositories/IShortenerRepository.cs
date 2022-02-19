using System.Collections.Generic;
using System.Threading.Tasks;
using UrlShortener.Models;

namespace UrlShortener.Repositories
{
    public interface IShortenerRepository
    {
        Task<UrlsModel> InsertOrGetExistingUrlAsync(string fullUrl);
        Task<List<UrlsModel>> GetAllUrlsAsync();
    }
}