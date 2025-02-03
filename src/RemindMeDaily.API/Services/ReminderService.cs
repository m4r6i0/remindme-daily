using RemindMeDaily.API.Models.Request;
using RemindMeDaily.API.Models.Response;
using RemindMeDaily.Domain.Entities;
using RemindMeDaily.API.Services.Interfaces;
using RemindMeDaily.Domain.Interfaces;
using RemindMeDaily.API.Extensions;

namespace RemindMeDaily.API.Services
{
    public class ReminderService : IReminderService
    {
        private readonly IReminderRepository _reminderRepository;


        public ReminderService(IReminderRepository repository)
        {
            _reminderRepository = repository;
        }

        public async Task<List<ReminderResponse>> GetAllRemindersAsync()
        {
            var reminders = await _reminderRepository.GetAllAsync();

            if (reminders is null || !reminders.Any())
                throw new KeyNotFoundException("Nenhum lembrete encontrado.");

            return reminders.Select(r => new ReminderResponse
            {
                Title = r.Title,
                Description = r.Description,
                ReminderDate = r.ReminderDate?.ToShortDateString()
            }).ToList();
        }

        public async Task<ReminderResponse> GetReminderByIdAsync(int id)
        {
            var reminder = await _reminderRepository.GetByIdAsync(id);

            if (reminder is null)
                throw new KeyNotFoundException("Nenhum lembrete encontrado.");

            return new ReminderResponse()
            {
                Title = reminder.Title,
                Description = reminder.Description,
                ReminderDate = reminder.ReminderDate?.ToFormattedString()
            };
        }

        public async Task<ReminderResponse> CreateReminderAsync(ReminderRequest request)
        {
            if (request is null)
                throw new ArgumentException("O lembrete não pode ser nulo.");

            var reminder = new Reminder
            (
                request.Title,
                request.Description,
                request.ReminderDate?.ToADateTime() ?? DateTime.MinValue
            );

            try
            {
                await _reminderRepository.AddAsync(reminder);

                return new ReminderResponse
                {
                    Title = reminder.Title,
                    Description = reminder.Description,
                    ReminderDate = reminder.ReminderDate?.ToFormattedString(),
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao incluir o lembrete. {ex.Message}", ex);
            }
        }

        public async Task<ReminderResponse> UpdateReminderAsync(ReminderRequest request, int id)
        {

            if (request is null)
                throw new ArgumentException("O lembrete não pode ser nulo.");

           if (id == 0)
                throw new ArgumentException("Informe o identificador do lembrete!");

            var reminder = await _reminderRepository.GetByIdAsync(id);

            if (reminder is null)
                throw new KeyNotFoundException($"Nenhum lembrete encontrado com ID {id}.");


            reminder.Title = request.Title;
            reminder.Description = request.Description;
            reminder.ReminderDate = request.ReminderDate?.ToADateTime();

            try
            {
                await _reminderRepository.UpdateAsync(reminder);

                return new ReminderResponse
                {
                    Title = reminder.Title,
                    Description = reminder.Description,
                    ReminderDate = reminder.ReminderDate?.ToFormattedString(),
                };                
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao alterar o lembrete com ID {id}: {ex.Message}", ex);
            }
        }

        public async Task DeleteReminderAsync(int id)
        {
            if (id == 0)
                throw new ArgumentException("Informe o identificador do lembrete!");

            var reminder = await _reminderRepository.GetByIdAsync(id);

            if (reminder is null)
                throw new KeyNotFoundException($"Nenhum lembrete encontrado com ID {id}.");

            try
            {
                await _reminderRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir o lembrete com ID {id}: {ex.Message}", ex);
            }
        }
    }
}
