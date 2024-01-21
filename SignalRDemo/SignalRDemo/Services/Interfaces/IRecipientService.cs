using SignalRDemo.Models;
using SignalRDemo.ModelView;

namespace SignalRDemo.Services.Interfaces
{
    public interface IRecipientService
    {
        Task<IEnumerable<Recipients>> GetRecipient();
        Task<Recipients> AddRecipient(RecipientView recipientView);
        Task<Recipients> DeleteRecipient(Guid Id);
        Task<Recipients> UpdateRecipient(Guid Id, RecipientView recipientView);
    }
}
