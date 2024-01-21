using Hangfire;
using Microsoft.AspNetCore.Mvc;
using SignalRDemo.Hubs;
//using MongoDB.Bson.IO;
using SignalRDemo.Models;
using SignalRDemo.ModelView;
using SignalRDemo.Services;
using SignalRDemo.Services.Interfaces;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace SignalRDemo.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class NotificationSettingController : Controller
    {
        private readonly INotificationSettingService _notificationSettingService;
        private readonly INotificationService _notificationService;
        NotificationHub _hub;
        public NotificationSettingController(INotificationSettingService notificationSettingService, INotificationService notificationService, NotificationHub notificationHub)
        {
            _notificationSettingService = notificationSettingService;
            _notificationService = notificationService;
            _hub = notificationHub;
        }
        [HttpPost("/notification-setting/schedule-job")]
        public async Task<IActionResult> AddNotificationSettingScheduleJob(NotificationSettingsView notificationSettingView)
        {
            try
            {
                var selectedDate = DateTimeOffset.Parse(notificationSettingView.TimeNotiSetting.ToString());
                var timeToSchedule = notificationSettingView.TimeNotiSetting;
                string cronExpression = $"{timeToSchedule.Day} - {timeToSchedule.Month} - {timeToSchedule.Year} at {timeToSchedule.Hour} : {timeToSchedule.Minute} : {timeToSchedule.Second}";

                var job = await _notificationSettingService.AddNotification(notificationSettingView);

                if(job != null)
                {
                    BackgroundJob.Schedule(
                    () => AddToNotification(), selectedDate
                   );
                }

               
                //var add = await _notificationService.AddNotification(notificationSettingView);
                return Ok($"Notification will send at {cronExpression}");
                

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while add.ErrorMessage:{ex}");
            }
        }
        [HttpPost("/notification-setting/immediate-job")]
        // test k cần đặt thời gian
        public async Task<IActionResult> AddNotificationSettingImmediateJob(NotificationSettingsView notificationSettingView)
        {
            try
            {
                var job = await _notificationSettingService.AddNotification(notificationSettingView);

                if (job != null)
                {
                    BackgroundJob.Enqueue(() => AddToNotification());
                }

                return Ok("Notification will be sent immediately.");
            }
            catch (Exception ex)
            {
                // Xử lý các trường hợp ngoại lệ
                throw new Exception($"An error occurred while adding. ErrorMessage: {ex}");
            }
        }

        [HttpPost("/notifications-setting/recurrence-job")]
        public async Task<IActionResult> AddNotificationSettingRecurringJob(NotificationSettingsView notificationSettingView)
        {
            try
            {

                //var triggerType = notificationSettingView.TriggerType;
                var cronExpression = notificationSettingView.TimeNotiSetting;
                
                var job = await _notificationSettingService.AddNotification(notificationSettingView);
                        if (job != null)
                        {
                            var jobID = BackgroundJob.Schedule(
                    () => AddToNotification(), cronExpression);
                            BackgroundJob.ContinueJobWith(jobID, () => _hub.SendNotificationToGroupTriggerType(notificationSettingView));
                        }
                        
                      


                        //    RecurringJob.AddOrUpdate<INotificationSettingService>("recurringJob",
                        //x => x.AddNotification(notificationSettingView),
                        //Cron.Daily(cronExpression));

                   

                
                //var add = await _notificationSettingService.AddNotification(notificationSettingView);
                //return Ok($"Notification will add after {timeToSchedule.Minute} minute");
                return Ok();

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while add.ErrorMessage:{ex}");
            }
        }

        [HttpGet("/notifications-setting/add-to-notification")]
        public async Task<IActionResult> AddToNotification()
        {
            try
            {
                var list = await _notificationSettingService.GetNotification();
                
                foreach(var item in list)
                {
                    await AddNotification(item);

                }

                return Ok(list);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while get.ErrorMessage:{ex}");
            }

        }

        private async Task AddNotification(NotificationSettings item)
        {
            try
            {
                var add = await _notificationService.AddNotiSettingToNoti(item);
                
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while add.ErrorMessage:{ex}");
            }
        }

        [HttpGet("/notification-settings")]
        public async Task<IActionResult> GetNotificationSettingView()
        {
            try
            {
                var array = await _notificationSettingService.GetNotificationSettingView();
                
                for (int i = 0; i <= array.Length - 1; i++)
                {
                    string json = JsonConvert.SerializeObject(array.GetValue(i));
                    Console.WriteLine(json);
                    
                    
                         
                }
                

                return Ok(array);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while get.ErrorMessage:{ex}");
            }

        }





        [HttpDelete("/notification-setting/{id}")]
        public async Task<IActionResult> DeleteNotification(Guid id)
        {
            try
            {
                var delete = await _notificationSettingService.DeleteNotification(id);
                if (delete != null)
                {
                    return Ok(delete);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("/notification-setting/{id}")]
        public async Task<IActionResult> UpdateNotification(Guid id, NotificationSettingsView notificationSettingView)
        {
            try
            {

                var update = await _notificationSettingService.UpdateNotification(id, notificationSettingView);
                if (update != null)
                {
                    return Ok(update);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
