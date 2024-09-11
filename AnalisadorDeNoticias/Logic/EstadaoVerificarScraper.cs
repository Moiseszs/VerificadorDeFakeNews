using AngleSharp;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.RecordModels;

namespace PrototipoAnalisadorDeNoticias.Logic
{
    public class EstadaoVerificarScraper : IScraper
    {

        private const string ESTADAO_SITENAME = "www.estadao.com.br";

        private News news { get; set; }

        public Search search { get; set; }

        public EstadaoVerificarScraper(News _news, Search _search)
        {
            news = _news;
            search = _search;
        }

        public async Task<string> GoogleSearchOfKeywords()
        {
            try
            {
                var searchList = await search.GetFromSpecificSite(ESTADAO_SITENAME);
                var selectedSearch = searchList.FirstOrDefault();
                if (selectedSearch == null) return null;
                return selectedSearch.link;
            }
            catch(Exception e)
            {
                return null;
                throw new Exception("Error finding results");
                
            }

            
        }


        public async Task<News> VerifyNews()
        {
            CheckingSource source = new CheckingSource();

            string pageLink = await GoogleSearchOfKeywords();

            if(pageLink == null)
            {
                return null;
            }

            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(pageLink);
            
            source.SourceSite = "Estadão Verifica";

            source.link = pageLink;

            source.relatedHeadlines = await GetRelatedInfo();

            string statement = "O Estadão Verifica investigou e concluiu que";


            if (document.Body.TextContent.Contains($"{statement}: é enganoso") ||
                document.Body.TextContent.Contains($"{statement}: é falso"))
            {
                source.News = news;
                source.Veridict = "enganoso";
                news.checkingSources.Add(source);
                return news;
            }
            else if (document.Body.TextContent
                .Contains($"{statement}: é verdadeiro") ||document.Body.TextContent
                .Contains($"{statement}: é verídico"))
            {
                source.News = news;
                source.Veridict = "verdadeiro";
                news.checkingSources.Add(source);
                return news;
            }
            else
            {
                source.News = news;
                source.Veridict = "indefinido";
                news.checkingSources.Add(source);
            }


            return news;
            
        }

        public async Task<List<string>> GetRelatedInfo()
        {
            string pageLink = await GoogleSearchOfKeywords();

            if (pageLink == null)
            {
                return null;
            }

            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(pageLink);

            var content = document.QuerySelectorAll(".items > a > .content > h3");

            List<string> headlines = new List<string>();

            foreach(var h3 in content)
            {
                headlines.Add(h3.TextContent);
            }

            return headlines;
        }
    }

    
}
