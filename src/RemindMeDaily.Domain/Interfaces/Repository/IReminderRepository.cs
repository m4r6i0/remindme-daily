using RemindMeDaily.Domain.Entities;

namespace RemindMeDaily.Domain.Interfaces.Repository;

public interface IReminderRepository
{
    Task<IEnumerable<Reminder>> GetAllAsync();
    Task<Reminder> GetByIdAsync(int id);
    Task AddAsync(Reminder reminder);
    Task UpdateAsync(Reminder reminder);
    Task DeleteAsync(int id);
}
