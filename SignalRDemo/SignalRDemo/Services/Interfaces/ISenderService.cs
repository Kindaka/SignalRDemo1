using SignalRDemo.Models;
using SignalRDemo.ModelView;


namespace SignalRDemo.Services.Interfaces
{
    public interface ISenderService
    {
        Task<IEnumerable<Senders>> GetSenders();
        Task<Senders> AddSender(SenderView senderView);
        Task<Senders> DeleteSender(Guid Id);
        Task<Senders> UpdateSender(Guid Id, SenderView senderView);
    }
}
