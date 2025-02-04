namespace RemindMeDaily.Domain.Models.Response
{
    public class ReminderResponse
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ReminderDate { get; set; }

        public ReminderResponse(string? title, string? description, string? reminderDate)
        {
            Title = title;
            Description = description;
            ReminderDate = reminderDate;
        }
    }
}
