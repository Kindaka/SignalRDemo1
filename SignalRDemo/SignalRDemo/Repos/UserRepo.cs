using Microsoft.EntityFrameworkCore;
using SignalRDemo.DBContext;
using SignalRDemo.Models;

namespace SignalRDemo.Repos
{
    public class UserRepo
    {
        private readonly NotificationDBContext dbContext;

        public UserRepo(NotificationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Recipients> GetUserDetails(string username, string password)
        {
            return await dbContext.Recipients.FirstOrDefaultAsync(user => user.RecipientName == username && user.PassWord == password);
        }
    }
}
