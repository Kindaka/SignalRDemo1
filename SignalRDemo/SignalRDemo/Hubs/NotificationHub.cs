using Hangfire;
using Microsoft.AspNetCore.SignalR;
using SignalRDemo.DBContext;
using SignalRDemo.Models;
using SignalRDemo.ModelView;

namespace SignalRDemo.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly NotificationDBContext _context;

        public NotificationHub(NotificationDBContext context)
        {
            this._context = context;
        }
        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendAsync("OnConnected");
            return base.OnConnectedAsync();
        }

        public async Task SaveUserConnection(string username)
        {
            var connectionId = Context.ConnectionId;
            HubConnections hubConnection = new HubConnections
            {
                ConnectionId = connectionId,
                RecipientName = username
            };

            _context.HubConnections.Add(hubConnection);
            await _context.SaveChangesAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var userConnection = _context.HubConnections.FirstOrDefault(con => con.ConnectionId == Context.ConnectionId)?.RecipientName;

            if (!string.IsNullOrEmpty(userConnection))
            {
                var hubConnectionsToRemove = _context.HubConnections.Where(con => con.RecipientName == userConnection).ToList();

                if (hubConnectionsToRemove.Any())
                {
                    _context.HubConnections.RemoveRange(hubConnectionsToRemove);
                    _context.SaveChangesAsync();
                }
            }
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendNotificationToGroup(string content, string userGroupId)
        {
            var hubConnections = _context.HubConnections.Join(_context.Recipients, c => c.RecipientName, o => o.RecipientName, (c, o) => new { c.RecipientName, c.ConnectionId, o.UserGroupId }).Where(o => o.UserGroupId.ToString() == userGroupId.ToString()).ToList();
            foreach (var hubConnection in hubConnections)
            {
                string username = hubConnection.RecipientName;
                if (!string.IsNullOrEmpty(hubConnection.ConnectionId))
                {
                    await Clients.Client(hubConnection.ConnectionId).SendAsync("ReceivedGroupNotification", content, username);
                }
                //Call Send Email function here
            }
        }

        public async Task SendNotificationToGroupTriggerType(NotificationSettingsView notificationSettingView)
        {
            var triggerType = notificationSettingView.TriggerType;
            switch (triggerType)
            {
                case "Minutely":

                    RecurringJob.AddOrUpdate("recurringJobMinutely", () => SendNotificationToGroup(notificationSettingView.Content, notificationSettingView.UserGroupId.ToString()), Cron.Minutely());
                    break;
                case "Daily":
                    RecurringJob.AddOrUpdate("recurringJobDaily", () => SendNotificationToGroup(notificationSettingView.Content, notificationSettingView.UserGroupId.ToString()), Cron.Daily());
                    break;
                case "Weekly":
                    RecurringJob.AddOrUpdate("recurringJobWeekly", () => SendNotificationToGroup(notificationSettingView.Content, notificationSettingView.UserGroupId.ToString()), Cron.Weekly());
                    break;
                case "Monthly":
                    RecurringJob.AddOrUpdate("recurringJobMonthly", () => SendNotificationToGroup(notificationSettingView.Content, notificationSettingView.UserGroupId.ToString()), Cron.Monthly());
                    break;


            }


        }
    }
}
