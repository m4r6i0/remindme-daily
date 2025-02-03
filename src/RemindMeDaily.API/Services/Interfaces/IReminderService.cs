using RemindMeDaily.API.Models.Request;
using RemindMeDaily.API.Models.Response;

namespace RemindMeDaily.API.Services.Interfaces;

public interface IReminderService
{
    Task<List<ReminderResponse>> GetAllRemindersAsync();
    Task<ReminderResponse> GetReminderByIdAsync(int id);
    Task<ReminderResponse> CreateReminderAsync(ReminderRequest reminder);
    Task<ReminderResponse> UpdateReminderAsync(ReminderRequest request, int id);
    Task DeleteReminderAsync(int id);
}
