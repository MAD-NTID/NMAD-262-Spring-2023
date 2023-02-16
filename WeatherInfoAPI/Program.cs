using Newtonsoft.Json;

const string IPSTACK_URL = "http://api.ipstack.com/check?access_key=fded42041e60ef1f4a2aa7b7b30dc51d";
const string OPEN_WEATHER_API_ENDPOINT = "https://api.openweathermap.org/data/2.5/weather?appid=22f949f4db660a93e36649e24ec04229&units=imperial";


Console.WriteLine("========================================");
Console.WriteLine("||          Weather APP                ||");
Console.WriteLine("========================================");
int choice = 0;
while(true){
    Console.WriteLine("1. Show the weather for my current location\n2. Show the weather for a different location and city");
    Console.Write("What would you like to do:");

    if(int.TryParse(Console.ReadLine(), out choice) && (choice==1 || choice==2))
        break;
    
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Invalid selection! Please select 1 or 2!");
    Console.ResetColor();

}

switch(choice)
{
    case 1:
    {
        dynamic response = await GetInfo(IPSTACK_URL);
        string openWeatherURL = OPEN_WEATHER_API_ENDPOINT + $"&lat={response.latitude}&lon={response.longitude}";
        await ShowWeatherInfo(openWeatherURL);

    }
    break;
    case 2:
    {
        Console.Write("State:");
        string state = Console.ReadLine();
        Console.WriteLine("City");
        string city = Console.ReadLine();

        string openWeatherURL = OPEN_WEATHER_API_ENDPOINT + $"&q={city},US,{state}";
        Console.Write(openWeatherURL);
        await ShowWeatherInfo(openWeatherURL);
 
    }
    break;
}

async Task ShowWeatherInfo(string url)
{
    dynamic info = await GetInfo(url);
    Console.WriteLine(info);

    Console.WriteLine($"Temp:{info.main.temp}");
    Console.WriteLine($"Feels like:{info.main.feels_like}");
    Console.WriteLine($"Lowest temp:{info.main.temp_min}");
    Console.WriteLine($"Highest temp:{info.main.temp_max}");
    Console.WriteLine($"Weather description:{info.weather[0].description}");
}



async Task<dynamic> GetInfo(string url)
{
    HttpClient client = new HttpClient();
    client.DefaultRequestHeaders.Add("User-Agent","Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36");
    HttpResponseMessage response = await client.GetAsync(url);
    response.EnsureSuccessStatusCode();
    string content = await response.Content.ReadAsStringAsync();
    return JsonConvert.DeserializeObject<dynamic>(content);

}