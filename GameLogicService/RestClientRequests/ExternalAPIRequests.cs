using GameLogicService.Models.Entity;
using GameLogicService.Models.ExternalResponse;
using GameLogicService.Models.Responses;
using GameLogicService.RestClientRequests.Interfaces;
using RestSharp;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace GameLogicService.RestClientRequests
{
    public class ExternalAPIRequests : BaseExternalAPIRequests, IExternalAPIRequests
    {
        public ExternalAPIRequests(HttpClient httpClient)
           : base(httpClient)
        {
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
