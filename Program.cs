using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebScraper
{
    class Program
    {
        private static int pagesToCheck;
        public static void Main(string[] args)
        {
            if (args.Count() > 0)
                HandleOptions(args);
            else
                GetPosts().GetAwaiter().GetResult();
        }

        //If no post number provided in args, set default to 0
        private static async Task GetPosts(int postsToReturn = 0)
        {
            var htmlbody = String.Empty;
            GetPagination(1, postsToReturn); //posts are paginated in groups of 30
            for (int i = 0; i < pagesToCheck; i++)
            {
                htmlbody+= await HttpUtil.GetWebPage(i + 1);       
            }
            //return IEnumberable of Newsarticles     
            var articles = WebScraperUtil.ExtractArticles(htmlbody).ToList();
            //If default(0) postToUse, just return everything from the first page 
            if (postsToReturn > 0 && (articles.Count >= postsToReturn))
            {   //Find the difference of the articles found and the postsToReturn argument, then remove the delta to return correct number of posts
                int d = articles.Count() - postsToReturn;
                articles.RemoveRange(articles.Count() - d, d);
            }
            Console.WriteLine(JsonConvert.SerializeObject(articles, Formatting.Indented));
        }

        /// <summary>
        /// Recursive Function to find number of pages to read to get the required number of posts
        /// </summary>
        /// <param name="pages"></param>
        /// <param name="posts"></param>
        private static void GetPagination(int pages, int posts)
        {
            if (posts > 30)
            {
                posts -= 30;
                pages++;
                GetPagination(pages, posts);
            }
            else
                pagesToCheck = pages;
        }

        /// <summary>
        /// Logic and error handling for cli args 
        /// </summary>
        /// <param name="args"></param>
        private static void HandleOptions(string[] args)
        {
            if (args.Count() > 2)
                Console.WriteLine("Too many arguments");
            else
            {
                switch (args[0])
                {
                    case "--posts":
                    case "-p": //Ensure that the n provided in the second arg is a valid int
                        if (args.Count() > 1 && Int32.TryParse(args[1], out int posts))
                        {
                            if (posts > 0 && posts <= 100) GetPosts(posts).GetAwaiter().GetResult();
                            else Console.WriteLine("Number of posts should be between 0 and equal or below 100");
                        }
                        else Console.WriteLine("Not a valid number");
                        break;
                    case "--help":
                    case "-h":
                        Console.WriteLine("\nHackerNews Webscraper\n");
                        Console.WriteLine("Options:\n--posts n\tposts how many posts to print. A positive integer <= 100.");
                        break;
                    default:
                        Console.WriteLine("Invalid argument, try --help for options");
                        break;
                }
            }
        }


    }
}
