using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace RestAPIMVC.Services;

public class RequestService
{
    private HttpClient client;
    public HttpRequestHeaders Headers
    {
        get => this.client.DefaultRequestHeaders;
    }

    public RequestService()
    {
        this.client = new HttpClient();
    }

    public async Task<dynamic> Post(string url, Dictionary<string, Object> body)
    {
        string jsonStr = JsonConvert.SerializeObject(body);
        var content = new StringContent(jsonStr, Encoding.UTF8, "application/json");

        var response = await this.client.PostAsync(url, content);

        return await this.ProcessRequest(response);
    }

    public async Task<dynamic> Get(string url)
    {
        var response = await this.client.GetAsync(url);
        return await this.ProcessRequest(response);
    }

    public async Task<dynamic> ProcessRequest(HttpResponseMessage response)
    {
        string content = await response.Content.ReadAsStringAsync();
        var info = JsonConvert.DeserializeObject<dynamic>(content);
        return info;
    }
}