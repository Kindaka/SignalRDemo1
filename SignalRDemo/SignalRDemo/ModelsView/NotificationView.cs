using System.ComponentModel.DataAnnotations;

namespace SignalRDemo.ModelView
{
    public class NotificationView
    {
        public DateTime SendAt { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Status { get; set; }
        public string? subject { get; set; }
        public string? content { get; set; }
        public enum NotiStatus
        {
            Success,
            Fail
        }
        public string NotiSetting_Id { get; set; }

        public string UserGroupId { get; set; }

        public string SenderId { get; set; }

    }
}
