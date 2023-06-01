using GameLogicService.RestClientRequests.Interfaces;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Net.Http.Headers;
using GameLogicService.Models.Responses;
using Azure;

namespace GameLogicService.RestClientRequests
{
    public class AuthAPIRequests : IAuthAPIRequests
    {
        protected readonly HttpClient _httpClient;
        protected readonly JsonSerializerOptions _options;

        public AuthAPIRequests(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://dev-he67eqpc846lev05.us.auth0.com/api/v2/");
            _options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                PropertyNameCaseInsensitive = true
            };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<bool> DeleteAuthUserData(string authId, string token)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", string.Format("Bearer {0}", token));
                var response = await _httpClient.DeleteAsync($"users/{authId}");
             Console.WriteLine(response.StatusCode.ToString());
             Console.WriteLine(response.Content.ToString());
             Console.WriteLine(response.Headers.ToString());
             Console.WriteLine(response.ReasonPhrase.ToString());
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            return true;
        }
    }
}
