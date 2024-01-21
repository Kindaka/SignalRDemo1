//using SignalR_MVC.SubscribeTableDependencies;
//using SignalR_MVC.Hubs;
//using SignalR_MVC.Models;
using TableDependency.SqlClient;
using SignalRDemo.Models;
using SignalRDemo.Hubs;

namespace SignalRDemo.SubscribeTableDependencies
{
    public class SubscribeNotificationTableDependency : ISubscribeTableDependency
    {
        SqlTableDependency<Notifications> tableDependency;
        NotificationHub notificationHub;

        public SubscribeNotificationTableDependency(NotificationHub notificationHub)
        {
            this.notificationHub = notificationHub;
        }

        public void SubscribeTableDependency(string connectionString)
        {
            tableDependency = new SqlTableDependency<Notifications>(connectionString);
            tableDependency.OnChanged += TableDependency_OnChanged;
            tableDependency.OnError += TableDependency_OnError;
            tableDependency.Start();
        }

        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(Notifications)} SqlTableDependency error: {e.Error.Message}");
        }

        private async void TableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Notifications> e)
        {
            //e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None
            if (e.ChangeType == TableDependency.SqlClient.Base.Enums.ChangeType.Insert) //ChangeType.Insert sự kiện chỉ được xử lý nếu có sự thêm mới (ChangeType.Insert).
                                                                                        //Các sự kiện cập nhật (ChangeType.Update) và xóa (ChangeType.Delete) sẽ bị bỏ qua.
            {
                var notification = e.Entity;

                await notificationHub.SendNotificationToGroup(notification.content, notification.UserGroupId);

            }
        }
    }
}
