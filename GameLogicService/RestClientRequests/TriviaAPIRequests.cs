using GameLogicService.Models.Entity;
using GameLogicService.Models.ExternalResponse;
using GameLogicService.Models.Responses;
using GameLogicService.RestClientRequests.Interfaces;
using RestSharp;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GameLogicService.RestClientRequests
{
    public class TriviaAPIRequests : ITriviaAPIRequests
    {
        protected readonly HttpClient _httpClient;
        protected readonly JsonSerializerOptions _options;

        public TriviaAPIRequests(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://getquizgamedata.azurewebsites.net");
            _options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                PropertyNameCaseInsensitive = true
            };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<GameCategoryExternalResponse>> GetGameCategories()
        {
            var response = await _httpClient.GetAsync($"/GetAllCategories");
            string json = await response.Content.ReadAsStringAsync();
            Console.WriteLine(json);
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var dictionary = JsonSerializer.Deserialize<Dictionary<string, string[]>>(json, options);
            var listOfGameCategories = new List<GameCategoryExternalResponse>();
            foreach (var item in dictionary)
            {
                listOfGameCategories.Add(new GameCategoryExternalResponse()
                {
                    CategoryName = item.Key,
                    CategoryTags = item.Value.ToList()
                });
            }
            return listOfGameCategories;
        }

        public async Task<List<GameQuestionExternalResponse>> GetGameQuestions(GameOptionsExternalResponse gameOptions)
        {
            var jsonRequestBody = JsonSerializer.Serialize(gameOptions);
            var httpContent = new StringContent(jsonRequestBody.ToLower(), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"/GetQuestions", httpContent);
            string json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var quizResults = JsonSerializer.Deserialize<GameQuizResultsExternalResponse>(json, options);
            return quizResults.Results.ToList() ?? new List<GameQuestionExternalResponse>();
        }

    }
}
