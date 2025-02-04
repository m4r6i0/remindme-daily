using RemindMeDaily.Domain.Entities;
using RemindMeDaily.Domain.Models.Request;
using RemindMeDaily.Domain.Models.Response;
using RemindMeDaily.API.Extensions;

namespace RemindMeDaily.API.DTOs.Extensions;

public static class ReminderDTOExtensions
{
    public static ReminderDTO ToReminderDTO(this Reminder reminder)
    {
        return new ReminderDTO
        (
            reminder.Id,
            reminder.Title,
            reminder.Description,
            reminder.ReminderDate
        );
    }

    public static List<ReminderDTO> ToListReminderDTO(this IEnumerable<Reminder> reminders)
    {
        return reminders.Select(reminder => ToReminderDTO(reminder)).ToList();
    }

    public static ReminderResponse ToReminderResponse(this ReminderDTO reminderDTO)
    {
        return new ReminderResponse
        (
            reminderDTO.Title,
            reminderDTO.Description,
            reminderDTO.ReminderDate?.ToFormattedString()
        );
    }

    public static List<ReminderResponse> ToListReminderResponse(this List<ReminderDTO> reminders)
    {
        return reminders.Select(dto => ToReminderResponse(dto)).ToList();
    }

    public static CreateReminderDTO ToCreateReminderDTO(this ReminderRequest request)
    {
        return new CreateReminderDTO
        (
            request.Title,
            request.Description,
            request.ReminderDate.ToADateTime()
        );
    }

    public static Reminder ToCreateReminder(this CreateReminderDTO request)
    {
        return new Reminder
        (
            request.Title,
            request.Description,
            request.ReminderDate
        );
    }

    public static ReminderResponse ToCreateReminderResponse(this Reminder reminder)
    {
        return new ReminderResponse
        (
            reminder.Title,
            reminder.Description,
            reminder.ReminderDate?.ToFormattedString()
        );
    }


    public static UpdateReminderDTO ToUpdateReminder(this ReminderRequest request, int id)
    {
        return new UpdateReminderDTO
        (
            id,
            request.Title,
            request.Description,
            request.ReminderDate.ToADateTime()
        );
    }
}
