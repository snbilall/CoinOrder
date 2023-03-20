using CoinOrderApi.Data.Models;
using CoinOrderApp.DtoModels.Request;

namespace CoinOrderApi.Providers
{
    public interface ICoinOrderProvider
    {
        Task CreateAsync(CreateOrderRequest request);
        Task<CoinOrder?> GetAsync(int userId);
        Task<bool> HasOrderAsync(int userId);
        Task DeleteAsync(int userId);
        Task<List<string>> GetCommunicationPermissionsAsync(Guid orderOd);
    }
}
