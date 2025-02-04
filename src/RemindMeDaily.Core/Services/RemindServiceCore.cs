using RemindMeDaily.Domain.Models.Request;
using RemindMeDaily.Domain.Models.Response;
using RemindMeDaily.Domain.Interfaces.Services;
using System.Net.Http.Json;
using RemindMeDaily.Core.Configurations;

namespace RemindMeDaily.Core.Services
{
    public class RemindServiceCore : IReminderCoreService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _settings;

        public RemindServiceCore(HttpClient httpClient, ApiSettings settings)
        {
            _httpClient = httpClient;
            _settings = settings;
        }

        public async Task<List<ReminderResponse>> GetAllRemindersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ReminderResponse>>(_settings.BaseUrl);
        }

        public async Task<ReminderResponse> GetReminderByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<ReminderResponse>($"{_settings.BaseUrl}/{id}");
        }

        public async Task<ReminderResponse> CreateReminderAsync(ReminderRequest reminder)
        {
            var response = await _httpClient.PostAsJsonAsync(_settings.BaseUrl, reminder);
            return await response.Content.ReadFromJsonAsync<ReminderResponse>();
        }

        public async Task<ReminderResponse> UpdateReminderAsync(int id, ReminderRequest reminder)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_settings.BaseUrl}/{id}", reminder);
            return await response.Content.ReadFromJsonAsync<ReminderResponse>();
        }

        public async Task<bool> DeleteReminderAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_settings.BaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
