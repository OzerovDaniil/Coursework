using System.Threading.Tasks;

namespace SushiManagementSystem.Application.Interfaces
{
    public interface ISettingsService
    {
        Task<object> GetSettingsAsync();
        Task UpdateSettingsAsync(string type, object settings);
    }
}