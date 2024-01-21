namespace SignalRDemo.Models.ViewModels
{
    public class NotificationListViewModel
    {
        public IEnumerable<NotificationViewModel> NotificationList { get; set; } = Enumerable.Empty<NotificationViewModel>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();
    }
}
