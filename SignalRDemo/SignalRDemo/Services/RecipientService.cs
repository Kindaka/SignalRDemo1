using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SignalRDemo.DBContext;
using SignalRDemo.Models;
using SignalRDemo.ModelView;
using SignalRDemo.Services.Interfaces;

namespace SignalRDemo.Services
{
    public class RecipientService : IRecipientService
    {
        protected readonly NotificationDBContext _context;
        private readonly IMapper _mapper;

        public RecipientService(NotificationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Recipients> AddRecipient(RecipientView recipientView)
        {
            try
            {
                var recipient = _mapper.Map<Recipients>(recipientView);

                _context.Recipients.Add(recipient);
                await _context.SaveChangesAsync();

                return recipient;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while adding recipient. ErrorMessage: {ex.Message}");
            }
        }

        public async Task<Recipients> DeleteRecipient(Guid Id)
        {
            try
            {
                var recipient = await _context.Recipients.FindAsync(Id);

                if (recipient == null)
                {
                    throw new Exception($"Recipient with ID {Id} not found.");
                }

                _context.Recipients.Remove(recipient);
                await _context.SaveChangesAsync();

                return recipient;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting recipient. ErrorMessage: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Recipients>> GetRecipient()
        {
            try
            {
                var recipients = await _context.Recipients.Select(r => new Recipients { RecipientName = r.RecipientName, RecipientId = r.RecipientId, UserGroupId = r.UserGroupId, PassWord = r.PassWord }).ToListAsync();
                return recipients;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching recipients. ErrorMessage: {ex.Message}");
            }
        }

        public async Task<Recipients> UpdateRecipient(Guid Id, RecipientView recipientView)
        {
            try
            {
                var existingRecipient = await _context.Recipients.FirstOrDefaultAsync(s => s.RecipientId == Id);

                if (existingRecipient != null)
                {
                    _mapper.Map(recipientView, existingRecipient);

                    await _context.SaveChangesAsync();

                    return existingRecipient;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating recipient. ErrorMessage: {ex.Message}");
            }
        }
    }
}
