using Microsoft.IdentityModel.Tokens;
using Models.RecordModels;
using System.Linq;
using System.Text;

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
            builder.Append(DotNetEnv.Env.GetString("BASE_URL"));
            builder.AppendJoin("", ["key=", DotNetEnv.Env.GetString("API_KEY")]);
            builder.AppendJoin("", ["&cx=", DotNetEnv.Env.GetString("SEARCH_ID")]);
            builder.AppendJoin("", ["&q=", SearchQuery]);
            return builder.ToString();
        }


        public async Task<List<Item>> GetFromSpecificSite(string displayLink)
        {
            return items.Where(obj => obj.displayLink == displayLink).ToList();
        }
    }
}
