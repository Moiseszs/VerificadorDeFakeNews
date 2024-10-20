using Microsoft.AspNetCore.Mvc;
using Models;
using Models.RecordModels;

namespace PrototipoAnalisadorDeNoticias.Services
{
    public abstract class Scraper
    {

        public string Sitename { get; set; }

        public virtual async Task<String> GoogleSearchOfKeywords() { return null;  }

        public virtual async Task<News> VerifyNews() { return null; }

        public virtual async Task<List<String>> GetRelatedInfo() { return null;  }
    }
}
