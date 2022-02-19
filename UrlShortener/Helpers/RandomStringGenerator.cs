using System;
using System.Linq;

namespace UrlShortener.Helpers
{
    public static class RandomStringGenerator
    {
        private static Random random = new Random();

        public static string GetRandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}