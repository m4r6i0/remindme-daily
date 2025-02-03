using System.ComponentModel.DataAnnotations;

namespace RemindMeDaily.API.Models.Request;

public class ReminderRequest
{
    [Required(ErrorMessage = "Titulo é obrigatório")]
    public string? Title { get; set; }
    [Required(ErrorMessage = "Descrição é obrigatória")]
    public string? Description { get; set; }
    [Required(ErrorMessage = "Data é obrigatório")]
    public string? ReminderDate { get; set; }
}
