using SignalRDemo.Models;
using SignalRDemo.ModelView;

namespace SignalRDemo.Services.Interfaces
{
    public interface INotificationSettingService
    {
        Task<IEnumerable<NotificationSettings>> GetNotification();

        Task<Array> GetNotificationSettingView();
        Task<NotificationSettings> AddNotification(NotificationSettingsView notificationSettingView);
        Task<NotificationSettings> DeleteNotification(Guid Id);
        Task<NotificationSettings> UpdateNotification(Guid Id, NotificationSettingsView notificationSettingView);


    }
}
