﻿using AngleSharp;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.RecordModels;
using System.Runtime.CompilerServices;

namespace PrototipoAnalisadorDeNoticias.Services
{
    public class EstadaoVerificarScraper : Scraper
    {


        private News news { get; set; }

        public Search search { get; set; }

        public EstadaoVerificarScraper(News _news, Search _search)
        {
            news = _news;
            search = _search;
            this.Sitename = "www.estadao.com.br";
        }

        public override async Task<string> GoogleSearchOfKeywords()
        {
            try
            {
                var searchList = await search.GetFromSpecificSite(this.Sitename);
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


        public override async Task<News> VerifyNews()
        {
            CheckingSource source = new CheckingSource();

            string pageLink = await GoogleSearchOfKeywords();


            if(pageLink == null)
            {
                return news;
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
                source.headline = document.Title;
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
                source.headline = document.Title;
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
