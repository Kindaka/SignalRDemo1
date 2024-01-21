using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalRDemo.Models
{
    public class UserGroups
    {


        //public Guid RecipientId { get; set; }

        //public Guid NotiSetting_Id { get; set; }

        //public Recipient? Recipient { get; set; }

        //public NotificationSettings? NotificationSetting { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserGroupId { get; set; }

        public string UserGroupName { get; set; }

        public ICollection<NotificationSettings>? NotificationSettings { get; set; }

        public ICollection<Recipients> recipients { get; set; }

    }
}
