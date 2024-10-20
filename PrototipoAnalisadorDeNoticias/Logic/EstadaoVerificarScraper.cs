﻿using AngleSharp;
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

        public async Task<String> GoogleSearchOfKeywords()
        {
            Search search = new Search(news.Title);
            try
            {
                await search.FetchQuery();
                var searchList = await search.GetFromSpecificSite(ESTADAO_SITENAME);
                var selectedSearch = searchList.FirstOrDefault();
                return selectedSearch.link;
            }
            catch(Exception e)
            {
                throw new Exception("Erro finding results");
            }
            
        }

        public async Task<News> VerifyNews()
        {
            string pageLink = "";
            CheckingSource source = new CheckingSource();
            try
            {
                pageLink = await GoogleSearchOfKeywords();
            }
            catch(Exception e)
            {
                source.Veridict = "Sem resultado";
                return news;
            }
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(pageLink);
            
            source.SourceSite = ESTADAO_SITENAME;

            if (document.Body.TextContent
                .Contains("O Estadão Verifica investigou e concluiu que: é enganoso") ||
                document.Body.TextContent
                .Contains("O Estadão Verifica investigou e concluiu que: é falso"))
            {
                source.News = news;
                source.Veridict = "enganoso";
                news.checkingSources.Add(source);
                return news;
            }
            else if (document.Body.TextContent
                .Contains("O Estadão Verifica investigou e concluiu que: é verdadeiro") ||document.Body.TextContent
                .Contains("O Estadão Verifica investigou e concluiu que: é veridico"))
            {
                source.News = news;
                source.Veridict = "verdadeiro";
                news.checkingSources.Add(source);
                return news;
            }
            source.News = news;
            source.Veridict = "indefinido";
            news.checkingSources.Add(source);
            return news;
            
        }
    }
}
