using Microsoft.AspNetCore.Mvc;
using Models;
using PrototipoAnalisadorDeNoticias.Services;
using AngleSharp;
using System.Reflection.Metadata;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using Models.RecordModels;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PrototipoAnalisadorDeNoticias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        // Mudar para GET?
        // POST api/<NewsController>
        [HttpPost("/")]
        public async Task<ActionResult<News>> Post([FromBody] News news)
        {

            Search search = new Search(news.Keywords);
            await search.FetchQuery();
            UolConfereScraper uolConfere = new UolConfereScraper(news, search);
            EstadaoVerificarScraper estadaoVerificar = new EstadaoVerificarScraper(news, search);
            news = await uolConfere.VerifyNews();
            news = await estadaoVerificar.VerifyNews();
            return news;


        }

        [HttpGet("/related-news")]
        public async Task<ActionResult> GetRelatedHeadlines()
        {
            UolConfereScraper uolConfere = new UolConfereScraper();
            var headlines = await uolConfere.GetRelatedInfo();
            
            return Ok(new { headlines });
        }

        }
    }

