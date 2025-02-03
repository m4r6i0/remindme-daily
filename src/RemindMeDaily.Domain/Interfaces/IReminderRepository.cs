using RemindMeDaily.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RemindMeDaily.Domain.Interfaces;

public interface IReminderRepository
{
    Task<IEnumerable<Reminder>> GetAllAsync();
    Task<Reminder> GetByIdAsync(int id);
    Task AddAsync(Reminder reminder);
    Task UpdateAsync(Reminder reminder);
    Task DeleteAsync(int id);
}
