using Microsoft.IdentityModel.Tokens;
using Models.RecordModels;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace PrototipoAnalisadorDeNoticias.Logic
{
    public class Search
    {
        public string SearchQuery { get; set; }

        public Search(string query)
        {
            SearchQuery = query;
        }

        public List<Item> items { get; set; }

        private string GetServerURL()
        {
            var confBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory());
            IConfiguration configuration = confBuilder.Build();

            return configuration.GetConnectionString("CUSTOMCONNSTR_API_URL");

        }

        public async Task<List<Item>> FetchQuery()
        {
            HttpClient httpClient = new HttpClient();
            var jsonObject = await httpClient.GetFromJsonAsync<JsonObject>(BuildUrl());
            httpClient.Dispose();
            items = jsonObject.items;
            return items;

        }


        public string BuildUrl()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendJoin("", GetServerURL());
            builder.AppendJoin("", ["&q=", SearchQuery]);
            return builder.ToString();
        }


        public async Task<List<Item>> GetFromSpecificSite(string displayLink)
        {
            return items.Where(obj => obj.displayLink == displayLink).ToList();
        }
    }
}
