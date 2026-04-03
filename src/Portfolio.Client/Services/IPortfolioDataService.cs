using Portfolio.Client.Models;

namespace Portfolio.Client.Services;

public interface IPortfolioDataService
{
    Task<PortfolioData?> GetDataAsync();
}
