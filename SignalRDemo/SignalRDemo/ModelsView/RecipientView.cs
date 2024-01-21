using System.ComponentModel.DataAnnotations;

namespace SignalRDemo.ModelView
{
    public class RecipientView
    {
        public string RecipientName { get; set; }

        public string PassWord { get; set; }

        public Guid UserGroupId { get; set; }
        
    }
}
