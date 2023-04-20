using System.Net.Http.Headers;
using RestAPIMVC.Models;

namespace RestAPIMVC.Services;

public class PaypalService
{
    private PaypalCredential _credential;
    private string baseUrl;
    private int version;
    private RequestService _requestService;

    public PaypalService(PaypalCredential credential, RequestService requestService)
    {
        this.SetVersion();
        this.baseUrl = "https://api-m.sandbox.paypal.com";
        this._credential = credential;
        this._requestService = requestService;
        this._requestService.Headers.Authorization = new AuthenticationHeaderValue("Basic", $"{this._credential.ClientId}:{this._credential.ClientSecret}");
        this._requestService.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public PaypalService SetVersion(int version = 2)
    {
        this.version = version;
        return this;
    }

    public async Task<dynamic> CreateOrder()
    {
        Dictionary<string, Object> body = new Dictionary<string, object>()
        {
            { "intent", "CAPTURE" },
            {
                "purchase_units",
                new Object[]
                {
                    new Dictionary<string, Object>()
                    {
                        {
                            "amount",
                            new Dictionary<string, Object>()
                            {
                                {
                                    "currency_code", "USD"
                                },
                                { "value", 100.00 }
                            }
                        },
                        {
                            "reference_id", Guid.NewGuid().ToString()
                        }
                    }
                }
            }
        };


        string url = $"{baseUrl}/v{version}/checkout/orders";
        return await this._requestService.Post(url, body);
    }
}