using Microsoft.AspNetCore.Authentication;
using RemindMeDaily.Domain.Entities;

namespace RemindMeDaily.API.DTOs
{
    public class ReminderDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? ReminderDate { get; set; }

        public ReminderDTO(int id, string? title, string? description, DateTime? reminderDate)
        {
            Id = id;
            Title = title;
            Description = description;
            ReminderDate = reminderDate;
        }
    }
}
