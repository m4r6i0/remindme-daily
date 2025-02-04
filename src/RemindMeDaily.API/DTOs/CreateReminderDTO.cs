namespace RemindMeDaily.API.DTOs
{
    public class CreateReminderDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReminderDate { get; set; }

        public CreateReminderDTO(string title, string description, DateTime RemindDate)
        {
            Title = title;
            Description = description;
            ReminderDate = RemindDate;
        } 

    }
}
