using AngleSharp;
using System.Net;

string proxyHost = "18.231.170.154:3128";

string UolConfereUrl = "https://noticias.uol.com.br/confere/";

var handler = new HttpClientHandler
{
    //Proxy = new WebProxy(proxyHost, true),
    PreAuthenticate = true,
    UseDefaultCredentials = false
};

var config = Configuration.Default.WithRequesters(handler).WithDefaultLoader();

var context = BrowsingContext.New(config);

var document = await context.OpenAsync(UolConfereUrl);

Console.WriteLine(document.Title);

while (true)
{

}


