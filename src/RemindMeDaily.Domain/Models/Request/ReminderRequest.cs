using System.ComponentModel.DataAnnotations;

namespace RemindMeDaily.Domain.Models.Request;

public class ReminderRequest
{
    [Required(ErrorMessage = "Titulo é obrigatório")]
    public string? Title { get; set; }
    [Required(ErrorMessage = "Descrição é obrigatória")]
    public string? Description { get; set; }
    [Required(ErrorMessage = "Data é obrigatório")]
    public string? ReminderDate { get; set; }

    public ReminderRequest(string title, string description, string? reminderDate)
    {
        Title = title;
        Description = description;
        ReminderDate = reminderDate;
    }
}
