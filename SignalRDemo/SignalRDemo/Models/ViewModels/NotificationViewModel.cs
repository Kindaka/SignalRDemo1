namespace SignalRDemo.Models.ViewModels
{
    public class NotificationViewModel
    {
        public string UserGroupId { get; set; }
        public string RecipientId { get; set; }
        public string UserGroupName { get; set; }
        public string RecipientName { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime SendAt { get; set; }

        //public PagingInfo PagingInfo { get; set; } = new PagingInfo();
    }
}
