using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Extensions.Logging; // Adicionando suporte para logs
using RemindMeDaily.Domain.Models.Response;
using RemindMeDaily.Domain.Interfaces.Services;

namespace RemindMeDaily.ViewModels
{
    public class RemindersViewModel : INotifyPropertyChanged
    {
        private readonly IReminderCoreService _reminderService;
        private readonly ILogger<RemindersViewModel> _logger;
        private bool _isLoading;

        public ObservableCollection<ReminderResponse> Reminders { get; set; } = new();

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
                _logger.LogInformation($"IsLoading atualizado para: {_isLoading}");
            }
        }

        public ICommand LoadRemindersCommand { get; }

        // Construtor sem parâmetros para evitar erro XFC0004 no XAML
        public RemindersViewModel()
        {
            throw new InvalidOperationException(
                "O construtor sem parâmetros é apenas para inicialização do XAML. Use a injeção de dependência.");
        }

        // Construtor com injeção de dependência
        public RemindersViewModel(IReminderCoreService reminderService, ILogger<RemindersViewModel> logger)
        {
            _reminderService = reminderService ?? throw new ArgumentNullException(nameof(reminderService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _logger.LogInformation("RemindersViewModel inicializado corretamente.");

            LoadRemindersCommand = new Command(async () => await LoadRemindersAsync());
        }

        public async Task LoadRemindersAsync()
        {
            _logger.LogInformation("Iniciando carregamento de lembretes...");
            var startTime = DateTime.UtcNow;

            try
            {
                IsLoading = true;

                var reminders = await _reminderService.GetAllRemindersAsync() ?? new List<ReminderResponse>();

                _logger.LogInformation($"Foram carregados {reminders.Count} lembretes.");

                Reminders.Clear();
                foreach (var reminder in reminders)
                {
                    Reminders.Add(reminder);
                    _logger.LogDebug($"Lembrete adicionado: {reminder.Description} - {reminder.Title}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao carregar lembretes.");
            }
            finally
            {
                IsLoading = false;
                var elapsedTime = DateTime.UtcNow - startTime;
                _logger.LogInformation($"Carregamento concluído em {elapsedTime.TotalMilliseconds} ms.");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            _logger.LogDebug($"Propriedade {propertyName} alterada.");
        }
    }
}
