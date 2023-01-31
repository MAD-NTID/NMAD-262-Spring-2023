using Newtonsoft.Json;

const string API_KEY = "51c506de40c84bfab9464d543aef3275";
string ipAddress = "";

string geoLookUpUrl = $"https://api.ipgeolocation.io/ipgeo?apiKey={API_KEY}&ip=";

//prompt the user for an valid IP address
while(true)
{
    Console.WriteLine("Enter an ip:");
    ipAddress = Console.ReadLine();

    if(!string.IsNullOrEmpty(ipAddress) && ipAddress.Split(".").Length == 4)
        break;
    
    Console.WriteLine("Invalid IP address");
}

HttpClient client = new HttpClient();

geoLookUpUrl = geoLookUpUrl+ipAddress;

HttpResponseMessage response = await client.GetAsync(geoLookUpUrl);
response.EnsureSuccessStatusCode();

string content = await response.Content.ReadAsStringAsync();
dynamic info = JsonConvert.DeserializeObject<dynamic>(content);
Console.WriteLine(info.city);


