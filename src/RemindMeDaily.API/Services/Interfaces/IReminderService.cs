using RemindMeDaily.API.DTOs;
using RemindMeDaily.Domain.Models.Response;

namespace RemindMeDaily.API.Interfaces;

public interface IReminderService
{
    Task<List<ReminderResponse>> GetAllRemindersAsync();
    Task<ReminderResponse> GetReminderByIdAsync(int id);
    Task<ReminderResponse> CreateReminderAsync(CreateReminderDTO createReminder);
    Task<ReminderResponse> UpdateReminderAsync(UpdateReminderDTO updateReminder);
    Task DeleteReminderAsync(int id);
}
