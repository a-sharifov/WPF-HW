using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3.Model
{
    public static class FilmSearchDownoloadService
    {
        private static string key = "6c31049f";
        private static string url = @"https://www.omdbapi.com/?t=";
        public static async Task<string> FindFilm(string name)
        {
            WebClient client = new WebClient();
            return await client.DownloadStringTaskAsync($@"{url}{name}&apikey={key}");
        }
    }
}
