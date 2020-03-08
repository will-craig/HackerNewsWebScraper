using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net.Http;

namespace WebScraper
{
    internal static class WebScraperUtil
    {
        private static string uri = "https://news.ycombinator.com/news?p=";
        private const string delimeter ="class='athing'";
        private const string expression = @"(?:""rank"">(?<rank>\d*)).+(?:href=""(?<uri>.+?)"").+(?:""storylink""(?:\s?rel=""nofollow"")?>(?<title>.+?)<\/a>).+(?:id=""score[\d_""]*>(?<points>.+?)points).+(?:""hnuser"">(?<author>.+?)<\/a>).+>(?:(?<comments>\d*)(?:&nbsp;|\s)?comments?|discuss)";
        
        /// <summary>
        /// Pass in html and sift through it with regex to pull out the useful data
        /// </summary>
        /// <param name="htmlBody"></param>
        /// <returns></returns>
        internal static IEnumerable<NewsArticle> ExtractArticles(string htmlBody)
        {
            //Easier for the regex to work if the data is split up into seperate articles 
            var htmlItems = htmlBody.Split(delimeter);
            var regex = new Regex(expression, RegexOptions.Singleline);
            foreach (var block in htmlItems)
            {
                Match match = regex.Match(block);
                // if the regex matched, mine out the named capture groups and return a collection of NewsArticles 
                if(match.Success)
                {                    
                    string title = Sanitize(match.Groups["title"].Value);
                    string uri = Sanitize(match.Groups["uri"].Value);
                    string author = Sanitize(match.Groups["author"].Value);
                    int points = Int32.TryParse(match.Groups["points"].Value.Trim(), out int p) ? p : 0;
                    int comment = Int32.TryParse(match.Groups["comments"].Value.Trim(), out int c) ? c : 0;
                    int rank = Int32.TryParse(match.Groups["rank"].Value.Trim(), out int r) ? r : 0;           
                    yield return new NewsArticle(title,uri,author,points,comment,rank);
                }        
            }
        }

        /// <summary>
        /// Ensure strings comply with rules for the JSON
        /// </summary>
        /// <returns></returns>
        private static string Sanitize(string value)
        {
            value = value.Trim();
            if (value.Length > 256)
            {
                value = value.Substring(0, 256);
            }
            return value;
        }

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
                return responseMessage.Content.ReadAsStringAsync().Result;
            }
        }

    }
}