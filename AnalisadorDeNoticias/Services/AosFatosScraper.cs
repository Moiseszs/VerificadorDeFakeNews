using AngleSharp;
using Models;
using PrototipoAnalisadorDeNoticias.Services;

namespace AnalisadorDeNoticias.Services
{
    public class AosFatosScraper : Scraper
    {

        public News news { get; set; }
        public Search search { get; set; }



        public AosFatosScraper(News _news, Search _search)
        {
            news = _news;
            search = _search;
            this.Sitename = "www.aosfatos.org";
        }

        public override async Task<string> GoogleSearchOfKeywords()
        {
            var searchList = await search.GetFromSpecificSite(this.Sitename);
            var selectedSearch = searchList.FirstOrDefault();
            return selectedSearch.link;
        }

        public override async Task<News> VerifyNews()
        {
            string pageUrl;
            try
            {
                pageUrl = await GoogleSearchOfKeywords();
            }
            catch(Exception e)
            {

                return news;
            }

            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(pageUrl);


            return null;
        }
    }
}
