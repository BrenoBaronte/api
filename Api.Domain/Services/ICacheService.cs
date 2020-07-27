using System.Threading.Tasks;

namespace Api.Domain.Services
{
    public interface ICacheService
    {
        Task<T> GetAsync<T>(string key) where T : class;
        Task SetAsync<T>(string key, object obj) where T : class;
    }
}
