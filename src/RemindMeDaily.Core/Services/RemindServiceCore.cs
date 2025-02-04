using RemindMeDaily.Domain.Models.Request;
using RemindMeDaily.Domain.Models.Response;
using RemindMeDaily.Domain.Interfaces.Services;
using RemindMeDaily.Core.Configurations;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;

namespace RemindMeDaily.Core.Services
{
    public class RemindServiceCore : IReminderCoreService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _settings;
        private readonly ILogger<RemindServiceCore> _logger;

        public RemindServiceCore(HttpClient httpClient, ApiSettings settings, ILogger<RemindServiceCore> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<ReminderResponse>> GetAllRemindersAsync()
        {
            _logger.LogInformation("Buscando lembretes na API: {BaseUrl}", _settings.BaseUrl);

            return await _httpClient.GetFromJsonAsync<List<ReminderResponse>>(_settings.BaseUrl);
        }

        public async Task<ReminderResponse> GetReminderByIdAsync(int id)
        {
            _logger.LogInformation("Buscando lembrete com ID {Id}", id);

            return await _httpClient.GetFromJsonAsync<ReminderResponse>($"{_settings.BaseUrl}/{id}");
        }

        public async Task<ReminderResponse> CreateReminderAsync(ReminderRequest reminder)
        {
            _logger.LogInformation("Criando lembrete: {Title}", reminder.Title);

            var response = await _httpClient.PostAsJsonAsync(_settings.BaseUrl, reminder);
            return await response.Content.ReadFromJsonAsync<ReminderResponse>();
        }

        public async Task<ReminderResponse> UpdateReminderAsync(int id, ReminderRequest reminder)
        {
            _logger.LogInformation("Atualizando lembrete ID {Id}", id);

            var response = await _httpClient.PutAsJsonAsync($"{_settings.BaseUrl}/{id}", reminder);
            return await response.Content.ReadFromJsonAsync<ReminderResponse>();
        }

        public async Task<bool> DeleteReminderAsync(int id)
        {
            _logger.LogInformation("Deletando lembrete ID {Id}", id);

            var response = await _httpClient.DeleteAsync($"{_settings.BaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
