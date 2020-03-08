namespace WebScraper
{
    internal class NewsArticle
    {
        public string title {get; private set; }
        public string uri { get; private set; }
        public string author { get; private set; }
        public int points { get; private set; }
        public int comment { get; private set; }
        public int rank { get; private set; }
    
        internal NewsArticle(string title, string uri, string author, int points, int comment, int rank)
        {
            this.title = title;
            this.uri = uri;
            this.author = author;
            this.points = points;
            this.comment = comment;
            this.rank = rank;
        }
    }
}