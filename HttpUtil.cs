using System.Net.Http;
using System.Threading.Tasks;

namespace WebScraper
{
    internal static class HttpUtil
    {     
        private const string uri = "https://news.ycombinator.com/news?p=";
        /// <summary>
        /// Http client method fetch and return HTML body content of Hacknew site 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        internal static async Task<string> GetWebPage(int page)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage responseMessage = await client.GetAsync(uri + page);
                responseMessage.EnsureSuccessStatusCode();
                return await responseMessage.Content.ReadAsStringAsync();
            }
        }
    }
}