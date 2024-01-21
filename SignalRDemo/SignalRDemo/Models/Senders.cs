using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalRDemo.Models
{
    public class Senders
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid SenderId { get; set; }

        public string SenderName { get; set; }

        public ICollection<NotificationSettings> Settings { get; set; }
    }
}
