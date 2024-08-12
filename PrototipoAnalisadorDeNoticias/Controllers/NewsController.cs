using Microsoft.AspNetCore.Mvc;
using Models;
using PrototipoAnalisadorDeNoticias.Logic;
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
        // GET: api/<NewsController>
        [HttpGet]
        public async Task<String> Get()
        {
            Search search = new Search("Biden");

            return "Teste";
        }

        // GET api/<NewsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            Search search = new Search("Pesquisas eleitorais");
            return search.BuildUrl();
        }

        // POST api/<NewsController>
        [HttpPost]
        public async Task<ActionResult<News>> Post([FromBody] News news)
        {
            Search search = new Search(news.Title);
            await search.FetchQuery();
            UolConfereScraper uolConfere = new UolConfereScraper(news, search);
            EstadaoVerificarScraper estadaoVerificar = new EstadaoVerificarScraper(news, search);
            news = await uolConfere.VerifyNews();
            news = await estadaoVerificar.VerifyNews();
            return news;
        }

        // PUT api/<NewsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // Exemplo de como usar Anglesharp para extrair info de uma pagina

        [HttpPut]
        public async Task<List<string>> Put()
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var doc = await context.OpenAsync("https://noticias.uol.com.br/confere/");
            var title = doc.QuerySelector("title")?.TextContent ?? "----";

            /*
             * Para interagir com uma classe CSS é necessário colocar o ponto ( . ) antes do nome.
             * Exemplo: cardElement = document.QuerySelector(".card")
             * 
             */
            
            var elements = doc.QuerySelectorAll(".thumb-title"); 
            List<string> texts = new(){ };
            foreach(var element in elements)
            {
                texts.Add(element.TextContent);
            }
            return texts;
        }

        // DELETE api/<NewsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
