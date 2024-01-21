using Microsoft.AspNetCore.Mvc;
using SignalRDemo.Models;
using SignalRDemo.ModelView;
using SignalRDemo.Services;
using SignalRDemo.Services.Interfaces;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace SignalRDemo.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class RecipientController : ControllerBase
    {
        private readonly IRecipientService _recipientService;
        public RecipientController( IRecipientService recipientService)
        {
            _recipientService = recipientService;
        }

        [HttpPost("/recipient")]
        public async Task<IActionResult> AddRecipient(RecipientView recipientView)
        {
            try
            {
                var add = await _recipientService.AddRecipient(recipientView);
                return Ok(add);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while add.ErrorMessage:{ex}");
            }
        }

        [HttpGet("/recipient")]
        public async Task<IActionResult> getAll()
        {
            try
            {
                var list = await _recipientService.GetRecipient();
                //var options = new JsonSerializerOptions
                //{
                //    ReferenceHandler = ReferenceHandler.Preserve,
                //    // Other options as needed
                //};

                //// Serialize the list using JsonSerializerOptions
                //string jsonString = JsonSerializer.Serialize(list, options);
                return Ok(list);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while get.ErrorMessage:{ex}");
            }

        }

        [HttpDelete("/recipient/{id}")]
        public async Task<IActionResult> DeleteRecipient(Guid id)
        {
            try
            {
                var delete = await _recipientService.DeleteRecipient(id);
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


        [HttpPut("recipient/{id}")]
        public async Task<IActionResult> UpdateRecipient(Guid id, RecipientView recipientView)
        {
            try
            {

                var update = await _recipientService.UpdateRecipient(id, recipientView);
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
