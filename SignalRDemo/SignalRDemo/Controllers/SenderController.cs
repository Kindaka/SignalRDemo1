using Hangfire;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using SignalRDemo.ModelView;
using SignalRDemo.Services;
using SignalRDemo.Services.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SignalRDemo.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class SenderController : ControllerBase
    {
        private readonly ISenderService _SenderService;
        public SenderController(ISenderService senderService)
        {
            _SenderService = senderService;
        }
        [HttpPost("/sender")]
        public async Task<IActionResult> AddSender(SenderView senderView)
        {
            try
            {
                var add = await _SenderService.AddSender(senderView);
                return Ok(add);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while add.ErrorMessage:{ex}");
            }
        }


        [HttpGet("/sender")]
        public async Task<IActionResult> getAll()
        {
            try
            {
                var list = await _SenderService.GetSenders();
                return Ok(list);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while get.ErrorMessage:{ex}");
            }

        }
        [HttpDelete("/sender/{id}")]
        public async Task<IActionResult> DeleteSender(Guid id)
        {
            //try
            //{
            //    //var delete = await _SenderService.DeleteSender(id);
            //    var selectedDate = DateTimeOffset.Parse(time.ToString());
            //    var jobID = BackgroundJob.Schedule<ISenderService>(x => x.DeleteSender(id), selectedDate);
            //    //Console.WriteLine(jobID);
            //    if (jobID != null)
            //    {
            //        return Ok($"Delete {jobID} successfully");
            //    }
            //    else
            //    {
            //        return NotFound();
            //    }

            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}
            try
            {
                var delete = await _SenderService.DeleteSender(id);
                if (delete != null)
                {
                    return Ok($"Delete {id} successfully");
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

        [HttpPut("/sender/{id}")]
        public async Task<IActionResult> UpdateSender(Guid id, SenderView senderView)
        {
            try
            {

                var update = await _SenderService.UpdateSender(id, senderView);
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
