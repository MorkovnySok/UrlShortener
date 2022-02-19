using Microsoft.Extensions.DependencyInjection;
using UrlShortener.MongoDB;
using UrlShortener.Repositories;

namespace UrlShortener
{
    public static class ServicesRegistrar
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IMongoHelper, MongoHelper>();
            services.AddTransient<IShortenerRepository, ShortenerRepository>();
        }
    }
}