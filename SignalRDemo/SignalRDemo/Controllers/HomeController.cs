using Microsoft.AspNetCore.Mvc;
using SignalRDemo.DBContext;
using SignalRDemo.Models;
using SignalRDemo.Models.ViewModels;
using System.Diagnostics;

namespace SignalRDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NotificationDBContext _notificationServiceContext;
        public int pageSize = 5;

        public HomeController(ILogger<HomeController> logger, NotificationDBContext notificationServiceContext)
        {
            _logger = logger;
            this._notificationServiceContext = notificationServiceContext;
        }

        public IActionResult Index()
        {
            string recipientID = HttpContext.Session.GetString("RecipientId");
            string userGroupId = HttpContext.Session.GetString("UserGroupId");

            //    var notificationListQuery = _notificationServiceContext.Recipients
            //.Join(
            //    _notificationServiceContext.UserGroups,
            //    recipient => recipient.UserGroupId,
            //    userGroup => userGroup.UserGroupId,
            //    (recipient, userGroup) => new { Recipient = recipient, UserGroup = userGroup }
            //)
            //.ToList() // Chuyển sang dạng có thể dịch được
            //.GroupJoin(
            //    _notificationServiceContext.Notifications,
            //    joined => joined.Recipient.UserGroupId.ToString(), // Thay thế bằng khóa chính thích hợp trong Recipients
            //    notification => notification.UserGroupId.ToString(), // Thay thế bằng khóa chính thích hợp trong Notifications
            //    (joined, notifications) => new { Recipient = joined.Recipient, UserGroup = joined.UserGroup, Notifications = notifications }
            //)
            //.SelectMany(
            //    joined => joined.Notifications.DefaultIfEmpty(),
            //    (joined, notification) => new NotificationViewModel
            //    {
            //        UserGroupName = joined.UserGroup.UserGroupName,
            //        UserGroupId = userGroupId,
            //        RecipientName = joined.Recipient.RecipientName,
            //        SendAt = notification.SendAt,
            //        Subject = notification.Subject,
            //        Content = notification.Content,
            //        // Thêm các thuộc tính khác nếu cần thiết, có thể là thuộc tính từ Notifications hoặc các bảng khác

            //    }).Where(n => n.RecipientId == recipientID).ToList();

            var notificationListQuery = _notificationServiceContext.Notifications
    .Join(
        _notificationServiceContext.UserGroups,
        noti => noti.UserGroupId,
        groups => groups.UserGroupId.ToString(),
        (noti, groups) => new { Notifications = noti, UserGroups = groups }).ToList()
    .Join(
        _notificationServiceContext.Recipients,
        joined => joined.UserGroups.UserGroupId,
        recipient => recipient.UserGroupId,
        (joined, recipient) => new { Notifications = joined.Notifications, UserGroup = joined.UserGroups, Recipient = recipient })
    .Where(joined => joined.Recipient.RecipientId.ToString() == recipientID) // Thêm điều kiện WHERE
    .Select(joined => new NotificationViewModel
    {
        UserGroupName = joined.UserGroup.UserGroupName,
        UserGroupId = joined.UserGroup.UserGroupId.ToString(), // Chuyển đổi UserGroupId về kiểu string
        RecipientName = joined.Recipient.RecipientName,
        SendAt = joined.Notifications.SendAt,
        Subject = joined.Notifications.subject,
        Content = joined.Notifications.content,
        // Thêm các thuộc tính khác nếu cần thiết, có thể là thuộc tính từ notifications hoặc các bảng khác
    }).OrderByDescending(joined => joined.SendAt)
    .ToList();

            return View(notificationListQuery);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}