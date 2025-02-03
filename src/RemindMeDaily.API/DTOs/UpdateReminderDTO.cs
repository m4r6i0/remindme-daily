namespace RemindMeDaily.API.DTOs
{
    public class UpdateReminderDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReminderDate { get; set; }
    }
}
