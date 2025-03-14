﻿using Microsoft.AspNetCore.Http.HttpResults;
using Models;
using OpenQA.Selenium;

using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Models.RecordModels;
using OpenQA.Selenium.DevTools.V125.WebAudio;
using System.Net;

namespace PrototipoAnalisadorDeNoticias.Services
{
    public class UolConfereScraper : Scraper
    {
        public News news { get; set; }
        private Search search;

        public UolConfereScraper(News _news, Search _search)
        {
            news = _news;
            search = _search;
            this.Sitename = "noticias.uol.com.br";
        }
        
        public UolConfereScraper()
        {

        }

        public override async Task<String> GoogleSearchOfKeywords()
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
                    source.headline = document.Title;
                    news.checkingSources.Add(source);
                    return news;
                }
                else if (textContent.Contains("distorcido"))
                {
                    source.News = news;
                    source.Veridict = "distorcido";
                    source.headline = document.Title;
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


        // A ser removida
        public async Task<List<string>> GetRelatedInfo()
        {
            var url = "https://noticias.uol.com.br/confere/";
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(url);


            var content = document.
                QuerySelectorAll("body > div.collection-standard > section.latest-news-banner > section > div > div > div.col-sm-24.col-md-16.col-lg-17 > section > div > div > div > div > div > div > a > div > h3");
            //content = content;
            var title = document.Title;

            List<string> headlines = new List<string>();

            foreach (var div in content)
            {
                headlines.Add(div.TextContent);
            }


            return headlines;
        }

    }


}
