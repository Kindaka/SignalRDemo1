using SignalRDemo.Models;
using SignalRDemo;
namespace SignalRDemo.ModelsView
{
    public class NotificationViewModel
    {
        public string? Subject { get; set; }
        public string? Content { get; set; }
        public DateTime SendAt { get; set; }
        public Senders SenderName { get; set; }
    }

}
