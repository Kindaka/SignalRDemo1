using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalRDemo.Models
{
    public class Recipients
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public Guid RecipientId { get; set; }

        public string RecipientName { get; set; }

        public string PassWord { get; set; }

        public Guid UserGroupId { get; set; }

        public UserGroups UserGroup { get; set; }

        //public ICollection<UserGroup>? UserGroups { get; set; }
    }
}
