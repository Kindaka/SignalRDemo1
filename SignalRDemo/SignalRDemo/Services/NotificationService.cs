using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SignalRDemo.DBContext;
using SignalRDemo.Models;
using SignalRDemo.ModelView;
using SignalRDemo.Services.Interfaces;
//using ZstdSharp.Unsafe;

namespace SignalRDemo.Services
{
    public class NotificationService : INotificationService
    {
        protected readonly NotificationDBContext _context;
        private readonly IMapper _mapper;

        public NotificationService(NotificationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Notifications> AddNotification(NotificationView notificationView)
        {
            try
            {
                var notification = _mapper.Map<Notifications>(notificationView);

                _context.Notifications.Add(notification);
                await _context.SaveChangesAsync();

                return notification;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while adding notification. ErrorMessage: {ex.Message}");
            }
        }

        public async Task<Notifications> AddNotiSettingToNoti(NotificationSettings notificationSetting)
        {
            var notiSettingItem = new Notifications();
            var existingNotification = _context.Notifications.Select(n => n.NotiSetting_Id).ToList();
            if( !existingNotification.Contains(notificationSetting.NotiSetting_Id.ToString()))
            {
                notiSettingItem.NotiSetting_Id = notificationSetting.NotiSetting_Id.ToString();
                notiSettingItem.UserGroupId = notificationSetting.UserGroupId.ToString();
                notiSettingItem.SenderId = notificationSetting.SenderId.ToString();
                notiSettingItem.SendAt = DateTime.Now;
                notiSettingItem.FromDate = notificationSetting.StartDate;
                notiSettingItem.ToDate = notificationSetting.EndDate;
                notiSettingItem.Status = (Notifications.NotiStatus)notificationSetting.Status;
                notiSettingItem.subject = notificationSetting.Subject;
                notiSettingItem.content = notificationSetting.Content;

                _context.Notifications.Add(notiSettingItem);
                await _context.SaveChangesAsync();
            }
            
             
            
            
            
            return notiSettingItem;
        }

        public async Task<Notifications> DeleteNotification(Guid NotificationId)
        {
            try
            {
                var notification = await _context.Notifications.FindAsync(NotificationId);

                if (notification == null)
                {
                    throw new Exception($"Notification with ID {NotificationId} not found.");
                }

                _context.Notifications.Remove(notification);
                await _context.SaveChangesAsync();

                return notification;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting notification. ErrorMessage: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Notifications>> GetNotification()
        { 
            try
            {
                var notifications = await _context.Notifications.ToListAsync();
                return notifications;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching notification. ErrorMessage: {ex.Message}");
            }
        }

        public async Task<Notifications> UpdateNotification(Guid NotificationId, NotificationView notificationView)
        {
            try
            {
                var existingNotification = await _context.Notifications.FirstOrDefaultAsync(s => s.NotificationId == NotificationId);

                if (existingNotification != null)
                {
                    _mapper.Map(notificationView, existingNotification);

                    await _context.SaveChangesAsync();

                    return existingNotification;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating notificaiton. ErrorMessage: {ex.Message}");
            }
        }
    }
}
