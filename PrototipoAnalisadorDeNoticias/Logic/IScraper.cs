using Microsoft.AspNetCore.Mvc;
using Models;
using Models.RecordModels;

namespace PrototipoAnalisadorDeNoticias.Logic
{
    public interface IScraper
    {

        public Task<String> GoogleSearchOfKeywords();

        public Task<News> VerifyNews();

    }
}
