using GameLogicService.Models.ExternalResponse;
using GameLogicService.Models.Responses;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;
using GameLogicService.RestClientRequests.Interfaces;

namespace GameLogicService.RestClientRequests
{
    public class DeleteUserDataAPIRequests : IDeleteUserDataAPIRequests
    {
        protected readonly HttpClient _httpClient;
        protected readonly JsonSerializerOptions _options;

        public DeleteUserDataAPIRequests(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://game-scoreboard.azurewebsites.net");
            _options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                PropertyNameCaseInsensitive = true
            };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> DeleteUserData(string emailAddress)
        {
            var jsonRequestBody = JsonSerializer.Serialize(emailAddress);
            var httpContent = new StringContent(jsonRequestBody.ToLower(), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"/api/DeleteUser/DeleteUserInfo", httpContent);
            string json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var quizResults = JsonSerializer.Deserialize<string>(json, options);
            return quizResults ?? String.Empty;
        }
    }
}
