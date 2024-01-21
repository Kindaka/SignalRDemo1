using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using SignalRDemo.DBContext;
using SignalRDemo.Models;
using SignalRDemo.ModelView;
using SignalRDemo.Services.Interfaces;


namespace SignalRDemo.Services
{
    public class NotificationSettingService : INotificationSettingService
    {
        protected readonly NotificationDBContext _context;
        private readonly IMapper _mapper;

        public NotificationSettingService(NotificationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<NotificationSettings> AddNotification(NotificationSettingsView notificationSettingView)
        {
            try
            {
                var notificationSetting = _mapper.Map<NotificationSettings>(notificationSettingView);
                notificationSetting.CreateAt = DateTime.Now;
                notificationSetting.Status = (NotificationSettings.NotiStatus)Enum.Parse(typeof(NotificationSettings.NotiStatus), notificationSettingView.Status);
                notificationSetting.TriggerType = (NotificationSettings.Type)Enum.Parse(typeof(NotificationSettings.Type), notificationSettingView.TriggerType);
                notificationSetting.TimeNotiSetting = notificationSettingView.TimeNotiSetting;
                _context.NotificationSettings.Add(notificationSetting);
                await _context.SaveChangesAsync();

                return notificationSetting;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while adding notificationSetting. ErrorMessage: {ex.Message}");
            }
        }

        public async Task<NotificationSettings> DeleteNotification(Guid Id)
        {
            try
            {
                var notificationSetting = await _context.NotificationSettings.FindAsync(Id);

                if (notificationSetting == null)
                {
                    throw new Exception($"NotificationSetting with ID {Id} not found.");
                }

                _context.NotificationSettings.Remove(notificationSetting);
                await _context.SaveChangesAsync();

                return notificationSetting;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting notificationSetting. ErrorMessage: {ex.Message}");
            }
        }

        public async Task<IEnumerable<NotificationSettings>> GetNotification()
        {
            try
            {
                var notificationSetting = await _context.NotificationSettings.ToListAsync();
                return notificationSetting;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching notificationSetting. ErrorMessage: {ex.Message}");
            }
        }

        public async Task<Array> GetNotificationSettingView()
        {
            try
            {
                var notificationSetting = await _context.NotificationSettings.ToArrayAsync();
                //var lastArray = notificationSetting[notificationSetting.Length - 1];
                var simplifiedArray = notificationSetting.Select(ns => new
                {
                    ns.CreateAt,
                    ns.Status,
                    ns.TriggerType,
                    ns.TimeNotiSetting,
                    ns.Subject,
                    ns.Content
                    // ... (Chọn những thuộc tính bạn muốn giữ)
                }).ToArray();
                return simplifiedArray;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching notificationSetting. ErrorMessage: {ex.Message}");
            }
        }

        public async Task<NotificationSettings> UpdateNotification(Guid Id, NotificationSettingsView notificationSettingView)
        {
            try
            {
                var existingNotificationSetting = await _context.NotificationSettings.FirstOrDefaultAsync(s => s.NotiSetting_Id == Id);

                if (existingNotificationSetting != null)
                {
                    _mapper.Map(notificationSettingView, existingNotificationSetting);

                    await _context.SaveChangesAsync();

                    return existingNotificationSetting;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating notificationSetting. ErrorMessage: {ex.Message}");
            }
        }

  
    }
}
