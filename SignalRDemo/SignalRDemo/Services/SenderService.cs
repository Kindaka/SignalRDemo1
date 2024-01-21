using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SignalRDemo.DBContext;
using SignalRDemo.Models;
using SignalRDemo.ModelView;
using SignalRDemo.Services.Interfaces;

namespace SignalRDemo.Services
{
    public class SenderService : ISenderService
    {
        protected readonly NotificationDBContext _context;
        private readonly IMapper _mapper;

        public SenderService(NotificationDBContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<IEnumerable<Senders>> GetSenders()
        {
            try
            {
                var senders = await _context.Senders.ToListAsync();
                return senders;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching senders. ErrorMessage: {ex.Message}");
            }
        }

        public async Task<Senders>AddSender(SenderView senderView)
        {
            try
            {
                var sender = _mapper.Map<Senders>(senderView);

                _context.Senders.Add(sender);
                await _context.SaveChangesAsync();

                return sender;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while adding sender. ErrorMessage: {ex.Message}");
            }
        }

        public async Task<Senders> DeleteSender(Guid Id)
        {
            try
            {
                // Tìm Sender theo Id
                var sender = await _context.Senders.FindAsync(Id);

                if (sender == null)
                {
                    throw new Exception($"Sender with ID {Id} not found.");
                }

                // Xóa Sender từ DbContext
                _context.Senders.Remove(sender);
                await _context.SaveChangesAsync();

                return sender;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting sender. ErrorMessage: {ex.Message}");
            }
        }

        public async Task<Senders> UpdateSender(Guid Id, SenderView senderView)
        {
            try
            {
                // Kiểm tra xem Sender có tồn tại trong cơ sở dữ liệu không
                var existingSender = await _context.Senders.FirstOrDefaultAsync(s => s.SenderId == Id);

                if (existingSender != null)
                {
                    // Cập nhật thông tin Sender từ SenderView
                    _mapper.Map(senderView, existingSender);

                    // Lưu thay đổi vào DbContext
                    await _context.SaveChangesAsync();

                    return existingSender;
                }
                    return null;

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating sender. ErrorMessage: {ex.Message}");
            }
        }
    }
}
