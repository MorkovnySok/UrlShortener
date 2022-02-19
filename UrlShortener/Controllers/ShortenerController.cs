using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Controllers.Dtos;
using UrlShortener.Models;
using UrlShortener.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UrlShortener.Controllers
{
    [Route("")]
    [ApiController]
    public class ShortenerController : ControllerBase
    {
        private readonly IShortenerRepository _repository;

        public ShortenerController(IShortenerRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<List<UrlsModel>> Get()
        {
            return await _repository.GetAllUrlsAsync();
        }

        [HttpPost]
        public async Task<string> CreateOrGetUrl(CreateUrlDto input)
        {
            var result = await _repository.InsertOrGetExistingUrlAsync(input.FullUrl);
            var url = Url.Content("~/") + result.ShortUrl;
            return url;
        }
    }
}
