using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Observex.Core.Entities;
using Observex.Core.Enums;
using Observex.Core.Interfaces;
using Observex.Infrastructure.Data;

namespace Observex.Infrastructure.Repositories
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        public NotificationRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Notification>> GetNotificationsByDateAsync(DateTime date)
        {
            var startOfDay = date.Date;
            var endOfDay = startOfDay.AddDays(1);
            
            return await _context.Notifications
                .Where(n => n.CreatedAt >= startOfDay && n.CreatedAt < endOfDay)
                .Include(w => w.ReceiverWorker)
                .Include(so => so.SenderOfficer)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Notification>> GetNotificationsByTypeAsync(NotificationType notificationType)
        {
            return await _context.Notifications.Where(n => n.Type == notificationType)
                .Include(w => w.ReceiverWorker)
                .Include(so => so.SenderOfficer)
                .ToListAsync();
        }

        public async Task<IEnumerable<Notification>> GetWorkerNotificationsByIdAsync (Guid workerId)
        {
            return await _context.Notifications.Where(n => n.ReceiverWorkerId == workerId)
                .Include(so => so.SenderOfficer)
                .ToListAsync();
        }
    }
}
