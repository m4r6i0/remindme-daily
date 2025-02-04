using RemindMeDaily.API.DTOs;
using RemindMeDaily.API.DTOs.Extensions;
using RemindMeDaily.API.Interfaces;
using RemindMeDaily.Domain.Interfaces.Repository;
using RemindMeDaily.Domain.Models.Response;

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

            return reminders.ToListReminderDTO()
                            .ToListReminderResponse();
        }

        public async Task<ReminderResponse> GetReminderByIdAsync(int id)
        {
            var reminder = await _reminderRepository.GetByIdAsync(id);

            if (reminder is null)
                throw new KeyNotFoundException("Nenhum lembrete encontrado.");

            return reminder.ToReminderDTO()
                           .ToReminderResponse();
        }

        public async Task<ReminderResponse> CreateReminderAsync(CreateReminderDTO createReminder)
        {
            if (createReminder is null)
                throw new ArgumentException("O lembrete não pode ser nulo.");

            try
            {
                await _reminderRepository.AddAsync(createReminder.ToCreateReminder());
                return createReminder.ToCreateReminder()
                                     .ToCreateReminderResponse();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao incluir o lembrete. {ex.Message}", ex);
            }
        }

        public async Task<ReminderResponse> UpdateReminderAsync(UpdateReminderDTO updateReminder)
        {

            if (updateReminder is null)
                throw new ArgumentException("O lembrete não pode ser nulo.");

           if (updateReminder.Id == 0)
                throw new ArgumentException("Informe o identificador do lembrete!");

            var reminder = await _reminderRepository.GetByIdAsync(updateReminder.Id);

            if (reminder is null)
                throw new KeyNotFoundException($"Nenhum lembrete encontrado com ID {updateReminder.Id}.");


            reminder.Title = updateReminder.Title;
            reminder.Description = updateReminder.Description;
            reminder.ReminderDate = updateReminder.ReminderDate;

            try
            {
                await _reminderRepository.UpdateAsync(reminder);

                return reminder.ToCreateReminderResponse();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao alterar o lembrete com ID {updateReminder.Id}: {ex.Message}", ex);
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
