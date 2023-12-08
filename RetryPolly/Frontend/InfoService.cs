namespace RetryPolly;

using System.Net;
using Polly;
using Polly.Contrib.WaitAndRetry;

public class InfoService : IInfoService
{
    public static string ClientName = "infoService";
    private readonly IHttpClientFactory _clientFactory;

    public InfoService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<string> GetInfo()
    {
        var client = _clientFactory.CreateClient(ClientName);
        var info = await client.GetAsync("info");
        return await info.Content.ReadAsStringAsync();
    }
}
