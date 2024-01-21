using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalRDemo.Models
{
    public class NotificationSettings
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public Guid NotiSetting_Id { get; set; }
        
        public DateTime CreateAt { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public NotiStatus Status { get; set; }
        
        public Type TriggerType { get; set; }
       
        public DateTime TimeNotiSetting { get; set; }
        public string Subject { get; set; }

        public string Content { get; set; }

        public enum NotiStatus
        {
            Success,
            Fail
        }

        public enum Type
        {
            Minutely,
            Daily,
            Weekly,
            Monthly
        }

        public Guid SenderId { get; set; }

        public Guid UserGroupId { get; set; }

        public Senders Sender { get; set; }

        public UserGroups UserGroup { get; set; }

        //public ICollection<UserGroup>? UserGroups { get; set; }

    }
}
