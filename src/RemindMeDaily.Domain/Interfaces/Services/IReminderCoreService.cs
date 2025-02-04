using RemindMeDaily.Domain.Models.Request;
using RemindMeDaily.Domain.Models.Response;


namespace RemindMeDaily.Domain.Interfaces.Services;
public interface IReminderCoreService
{
    Task<List<ReminderResponse>> GetAllRemindersAsync();
    Task<ReminderResponse> GetReminderByIdAsync(int id);
    Task<ReminderResponse> CreateReminderAsync(ReminderRequest reminder);
    Task<ReminderResponse> UpdateReminderAsync(int id, ReminderRequest reminder);
    Task<bool> DeleteReminderAsync(int id);
}
