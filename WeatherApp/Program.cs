using HtmlAgilityPack;
using System.Net;

string weatherUrl = "https://forecast.weather.gov/MapClick.php?lat=43.1&lon=-77.5#.Y9ADUcnMJy8";

HttpClient client = new HttpClient();
client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (platform; rv:geckoversion) Gecko/geckotrail Firefox/firefoxversion");

HttpResponseMessage response = await client.GetAsync(weatherUrl);

response.EnsureSuccessStatusCode();

string html =  await response.Content.ReadAsStringAsync();
HtmlDocument parser = new HtmlDocument();
parser.LoadHtml(html);

string tempF = parser.DocumentNode.DescendantNodes().First(node=> node.HasClass("myforecast-current-lrg")).InnerText;
string tempC = parser.DocumentNode.DescendantNodes().First(node=> node.HasClass("myforecast-current-sm")).InnerText;

tempF = WebUtility.HtmlDecode(tempF);
tempC = WebUtility.HtmlDecode(tempC);
Console.WriteLine($"{tempF}/{tempC}");

