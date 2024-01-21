using System.ComponentModel.DataAnnotations;

namespace SignalRDemo.ModelView
{
    public class NotificationSettingsView
    {
        
        public DateTime CreateAt { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        
        public string TriggerType { get; set; }

        
        public DateTime TimeNotiSetting { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public Guid SenderId { get; set; }

        public Guid UserGroupId { get; set; }

        
    }
}
