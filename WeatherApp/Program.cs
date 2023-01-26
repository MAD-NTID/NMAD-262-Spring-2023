using HtmlAgilityPack;
using System.Net;


async Task<string> GetContent(string url)
{
    try{
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (platform; rv:geckoversion) Gecko/geckotrail Firefox/firefoxversion");
        HttpResponseMessage response = await client.GetAsync(url);
        return await response.Content.ReadAsStringAsync();
    }catch(Exception e)
    {
        Console.WriteLine($"Error: {e.Message}");
    }

    return "";


}

//setting up the urls
//zip code url
string longLatFinder = "https://www.zipinfo.com/cgi-local/zipsrch.exe?ll=ll&zip=";
string weatherUrl = $"https://forecast.weather.gov/MapClick.php?";
int zip = 0;
while(true)
{
     Console.WriteLine("Enter a zip code:");
     string zipString = Console.ReadLine();
    if(int.TryParse(zipString, out zip) && zipString.Length==5)
        break;
    
    Console.WriteLine("Invalid zip code!");
}





longLatFinder+=zip;
string html =  await GetContent(longLatFinder);
if(string.IsNullOrEmpty(html))
    return ;

HtmlDocument parser = new HtmlDocument();
parser.LoadHtml(html);

HtmlNode table = parser.DocumentNode.Descendants("table").ElementAt(3);
HtmlNode tr1 = table.Descendants("tr").ElementAt(0);
HtmlNode tr2 = table.Descendants("tr").ElementAt(1);





//parsing out lat and long
double lat = Convert.ToDouble(tr2.Descendants("td").ElementAt(3).InnerText);
double lon = Convert.ToDouble(tr2.Descendants("td").ElementAt(4).InnerText);

//decide when to add -1 value to the lon/lat
string latStr = tr1.Descendants("th").ElementAt(3).InnerText.ToLower();
string lontStr = tr1.Descendants("th").ElementAt(4).InnerText.ToLower();

if(lontStr.Contains("longitude") && lontStr.Contains("west")){
    lon*=-1;
}

if(latStr.Contains("latitude") && latStr.Contains("south")){
    lat*=-1;
}

Console.WriteLine($"{lon} {lat}");



weatherUrl+=$"lat={lat}&lon={lon}";
html =  await GetContent(weatherUrl);
if(string.IsNullOrEmpty(html))
    return ;
parser.LoadHtml(html);

string tempF = parser.DocumentNode.DescendantNodes().First(node=> node.HasClass("myforecast-current-lrg")).InnerText;
string tempC = parser.DocumentNode.DescendantNodes().First(node=> node.HasClass("myforecast-current-sm")).InnerText;




tempF = WebUtility.HtmlDecode(tempF);
tempC = WebUtility.HtmlDecode(tempC);
Console.WriteLine($"{tempF}/{tempC}");

