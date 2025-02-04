using Microsoft.AspNetCore.Mvc;
using RemindMeDaily.Domain.Models.Request;
using RemindMeDaily.Domain.Models.Response;
using RemindMeDaily.API.Interfaces;
using RemindMeDaily.API.DTOs.Extensions;


namespace RemindMeDaily.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RemindersController : ControllerBase
    {
        private readonly IReminderService _service;

        public RemindersController(IReminderService service)
        {
            _service = service;
        }

        // GET: /Reminders
        [HttpGet]
        public async Task<ActionResult<List<ReminderResponse>>> GetReminders()
        {
            var response = await _service.GetAllRemindersAsync();
            return Ok(response);
        }

        // POST: /Reminder
        [HttpPost]
        public async Task<ActionResult<ReminderResponse>> PostReminder([FromBody] ReminderRequest reminder)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new 
                {
                    Message = "Validação Falhou",
                    Errors = ModelState.Values
                        .SelectMany(v=> v.Errors)
                        .Select(e=> e.ErrorMessage).ToList()
                });
            }

            var response = await _service.CreateReminderAsync(reminder.ToCreateReminderDTO());
            return Ok(response);
        }

        // GET: /Reminders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReminderResponse>> GetReminder(int id)
        {
            var response = await _service.GetReminderByIdAsync(id);
            return Ok(response);
        }

        // PUT: /5/Reminder
        [HttpPut("{id}")]
        public async Task<ActionResult<ReminderResponse>> PutReminder([FromBody] ReminderRequest reminder, int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new 
                {
                    Message = "Validação Falhou",
                    Errors = ModelState.Values
                        .SelectMany(v=> v.Errors)
                        .Select(e=> e.ErrorMessage).ToList()
                });
            }

            var response = await _service.UpdateReminderAsync(reminder.ToUpdateReminder(id));
            return Ok(response);
        }

        // DELETE: /5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReminder(int id)
        {
            await _service.DeleteReminderAsync(id);
            return Ok(new {Message = "Registro Apagado com sucesso!"});
        }
    }
}
