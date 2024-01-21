using SignalRDemo.Models;
using SignalRDemo.ModelView;

namespace SignalRDemo.Services.Interfaces
{
    public interface INotificationService
    {
        Task<IEnumerable<Notifications>> GetNotification ();
        Task<Notifications> AddNotification(NotificationView notificationView);

        Task<Notifications> AddNotiSettingToNoti(NotificationSettings notificationSetting);
        Task<Notifications> DeleteNotification(Guid NotificationId);
        Task<Notifications> UpdateNotification(Guid NotificationId, NotificationView notificationView);
    }
}
