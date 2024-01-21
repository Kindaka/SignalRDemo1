using Microsoft.AspNetCore.Mvc;
using SignalRDemo.ModelView;
using SignalRDemo.Services;
using SignalRDemo.Services.Interfaces;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace SignalRDemo.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class UserGroupController : ControllerBase
    {
        private readonly IUserGroupService _userGroupService;
        public UserGroupController(IUserGroupService userGroupService)
        {
            _userGroupService = userGroupService;
        }

        [HttpPost("/usergroup")]
        public async Task<IActionResult> AddUserGroup(UserGroupView userGroupView)
        {
            try
            {
                var add = await _userGroupService.AddUserGroup(userGroupView);
                return Ok(add);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while add.ErrorMessage:{ex}");
            }
        }

        [HttpGet("/usergroup")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var list = await _userGroupService.GetUserGroup();
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

        [HttpDelete("/usergroup/{UserGroupId}")]
        public async Task<IActionResult> DeleteUserGroup(Guid UserGroupId)
        {
            try
            {
                var delete = await _userGroupService.DeleteUserGroup(UserGroupId);
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


        [HttpPut("/usergroup/{UserGroupId}")]
        public async Task<IActionResult> UpdateUserGroup(Guid UserGroupId, UserGroupView userGroupView)
        {
            try
            {

                var update = await _userGroupService.UpdateUserGroup(UserGroupId, userGroupView);
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
