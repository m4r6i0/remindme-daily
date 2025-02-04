using RemindMeDaily.API.Data;
using RemindMeDaily.Domain.Entities;
using RemindMeDaily.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;


namespace RemindMeDaily.API.Repositories
{
    public class ReminderRepository : IReminderRepository
    {
        private readonly AppDbContext _context;

        public ReminderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reminder>> GetAllAsync()
        {
            return await _context.Reminders.ToListAsync();
        }

        public async Task<Reminder> GetByIdAsync(int id)
        {
            return await _context.Reminders.FindAsync(id);
        }

        public async Task AddAsync(Reminder reminder)
        {
            await _context.Reminders.AddAsync(reminder);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Reminder reminder)
        {
            _context.Entry(reminder).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var reminder = await _context.Reminders.FindAsync(id);
            if (reminder != null)
            {
                _context.Reminders.Remove(reminder);
                await _context.SaveChangesAsync();
            }
        }
    }
}
