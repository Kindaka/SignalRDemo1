using SignalRDemo.Models;
using SignalRDemo.ModelView;

namespace SignalRDemo.Services.Interfaces
{
    public interface IUserGroupService
    {
        Task<IEnumerable<UserGroups>> GetUserGroup();
        Task<UserGroups> AddUserGroup(UserGroupView userGroupView);
        Task<UserGroups> DeleteUserGroup(Guid UserGroupId);
        Task<UserGroups> UpdateUserGroup(Guid UserGroupId, UserGroupView userGroupView);
    }
}
