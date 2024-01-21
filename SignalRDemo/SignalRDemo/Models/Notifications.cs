using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalRDemo.Models
{
    public class Notifications
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public Guid NotificationId { get; set; }

        public DateTime SendAt { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public NotiStatus Status { get; set; }
        public string subject { get; set; }
        public string content { get; set; }
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
