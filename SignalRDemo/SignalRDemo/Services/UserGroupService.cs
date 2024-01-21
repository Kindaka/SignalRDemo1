using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SignalRDemo.DBContext;
using SignalRDemo.Models;
using SignalRDemo.ModelView;
using SignalRDemo.Services.Interfaces;

namespace SignalRDemo.Services
{
    public class UserGroupService : IUserGroupService
    {
        protected readonly NotificationDBContext _context;
        private readonly IMapper _mapper;

        public UserGroupService(NotificationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<UserGroups> AddUserGroup(UserGroupView userGroupView)
        {
            try
            {
                
                
                    var usergroup = _mapper.Map<UserGroups>(userGroupView);
                    _context.UserGroups.Add(usergroup);

                    //_context.Entry(usergroup).Reference(u => u.NotificationSetting).Load();
                    //_context.Entry(usergroup).Reference(u => u.Recipient).Load();


                    
                    await _context.SaveChangesAsync();

                    return usergroup;
                
                
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while adding usergroup. ErrorMessage: {ex.Message}");
            }
        }

        public async Task<UserGroups> DeleteUserGroup(Guid UserGroupId)
        {
            try
            {
                var usergroup = await _context.UserGroups.FirstOrDefaultAsync(u => u.UserGroupId == UserGroupId);

                if (usergroup == null)
                {
                    throw new Exception($"Usergroup with UserGroupId {UserGroupId} not found.");
                }

                _context.UserGroups.Remove(usergroup);
                await _context.SaveChangesAsync();

                return usergroup;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting usergroup. ErrorMessage: {ex.Message}");
            }
        }

        public async Task<IEnumerable<UserGroups>> GetUserGroup()
        {
            try
            {
                var usergroup = await _context.UserGroups.Select(u => new UserGroups { UserGroupId = u.UserGroupId, UserGroupName = u.UserGroupName }).ToListAsync();
                return usergroup;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching usergroup. ErrorMessage: {ex.Message}");
            }
        }

        public async Task<UserGroups> UpdateUserGroup(Guid UserGroupId, UserGroupView userGroupView)
        {
            try
            {
                var existingUserGroup = await _context.UserGroups.FirstOrDefaultAsync(s => s.UserGroupId == UserGroupId);

                if (existingUserGroup != null)
                {
                    _mapper.Map(userGroupView, existingUserGroup);

                    await _context.SaveChangesAsync();

                    return existingUserGroup;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating userGroup. ErrorMessage: {ex.Message}");
            }
        }
    }
}
