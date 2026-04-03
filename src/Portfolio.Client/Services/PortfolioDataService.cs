using System.Text.Json;
using Portfolio.Client.Models;

namespace Portfolio.Client.Services;

public class PortfolioDataService : IPortfolioDataService
{
    private readonly HttpClient _http;
    private PortfolioData? _cache;

    public PortfolioDataService(HttpClient http)
    {
        _http = http;
    }

    public async Task<PortfolioData?> GetDataAsync()
    {
        if (_cache is not null)
            return _cache;

        var json = await _http.GetStringAsync("data/portfolio.json");
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        _cache = JsonSerializer.Deserialize<PortfolioData>(json, options);
        return _cache;
    }
}
