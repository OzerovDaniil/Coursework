using System.Threading.Tasks;
using SushiManagementSystem.Application.Interfaces;

namespace SushiManagementSystem.Application.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SettingsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> GetSettingsAsync()
        {
            // В реальному додатку тут має бути логіка отримання налаштувань з бази даних
            // Поки повертаємо тестові дані
            return new
            {
                general = new
                {
                    restaurantName = "Суші-Майстер",
                    contactPhone = "+380(68)-XXX-XX-XX",
                    workingHours = "10:00 - 22:00"
                },
                system = new
                {
                    enableNotifications = true,
                    autoBackup = true,
                    backupFrequency = "daily"
                },
                security = new
                {
                    passwordExpiry = 90,
                    twoFactorAuth = false,
                    sessionTimeout = 30
                }
            };
        }

        public async Task UpdateSettingsAsync(string type, object settings)
        {
            // В реальному додатку тут має бути логіка збереження налаштувань в базу даних
            // Поки просто повертаємо успішний результат
            await Task.CompletedTask;
        }
    }
}