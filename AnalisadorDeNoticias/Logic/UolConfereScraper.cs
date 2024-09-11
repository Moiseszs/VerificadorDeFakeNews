using Microsoft.AspNetCore.Http.HttpResults;
using Models;
using OpenQA.Selenium;

using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Models.RecordModels;
using OpenQA.Selenium.DevTools.V125.WebAudio;

namespace PrototipoAnalisadorDeNoticias.Logic
{
    public class UolConfereScraper 
    {

        private const string UOL_SITENAME = "noticias.uol.com.br";
        public News news { get; set; }
        private Search search;

        public UolConfereScraper(News _news, Search _search)
        {
            news = _news;
            search = _search;
        }

        public async Task<String> GoogleSearchOfKeywords()
        {
            
            var searchList = await search.GetFromSpecificSite(UOL_SITENAME);
            var selectedSearch = searchList.FirstOrDefault();
            return selectedSearch.link;

        }

        public async Task<News> VerifyNews()
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
            var bulletDivs = document.QuerySelectorAll("h2.bullet");
            CheckingSource source = new CheckingSource();
            source.SourceSite = "Uol Confere";

            source.relatedHeadlines = await GetRelatedInfo();

            source.link = pageUrl;

            foreach(var h in bulletDivs)
            {
                var textContent = h.TextContent;
                if (textContent.Contains("falso"))
                {
                    source.News = news;
                    source.Veridict = "enganoso";
                    news.checkingSources.Add(source);
                    return news;
                }
                else if (textContent.Contains("distorcido"))
                {
                    source.News = news;
                    source.Veridict = "distorcido";
                    news.checkingSources.Add(source);
                    return news;
                }
                else if (textContent.Contains("verdadeiro"))
                {
                    source.News = news;
                    source.Veridict = "verdadeiro";
                    news.checkingSources.Add(source);
                    return news;
                }
            }
            source.News = news;
            source.Veridict = "indefinido";
            news.checkingSources.Add(source);
            return news;
        }

        public async Task<List<string>> GetRelatedInfo()
        {

            string pageLink = await GoogleSearchOfKeywords();


            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(pageLink);


            var content = document.
                QuerySelectorAll(".mt-100.mt-150-lg.container  .solar-related.type-main  .container-main.column div");

            content = content;


            List<string> headlines = new List<string>();

            foreach(var div in content)
            {
                headlines.Add(div.TextContent);
            }

            return headlines;
        }

    }


}
