namespace RemindMeDaily.Domain.Entities;

public class Reminder
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? ReminderDate { get; set; }

    public Reminder() { }

    public Reminder(string title, string description, DateTime reminderDate)
    {
        Title = title;
        Description = description;  
        ReminderDate = reminderDate;
    }
}
