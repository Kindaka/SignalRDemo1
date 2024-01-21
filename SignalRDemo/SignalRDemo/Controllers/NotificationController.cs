using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SignalRDemo.DBContext;
using SignalRDemo.Models;
using SignalRDemo.ModelView;
using SignalRDemo.Services.Interfaces;

namespace SignalRDemo.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost("/notifications")]
        public async Task<IActionResult> AddNotification(NotificationView notificationView)
        {
            try
            {
                var add = await _notificationService.AddNotification(notificationView);
                return Ok(add);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while add.ErrorMessage:{ex}");
            }
        }

        [HttpGet("/notifications")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var list = await _notificationService.GetNotification();
                return Ok(list);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while get.ErrorMessage:{ex}");
            }
        }

        [HttpDelete("/notifications/{NotificationId}")]
        public async Task<IActionResult> DeleteNotification(Guid NotificationId)
        {
            try
            {
                var delete = await _notificationService.DeleteNotification(NotificationId);
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


        [HttpPut("/notifications/{id}")]
        public async Task<IActionResult> UpdateNotification(Guid id, NotificationView notificationView)
        {
            try
            {

                var update = await _notificationService.UpdateNotification(id, notificationView);
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
