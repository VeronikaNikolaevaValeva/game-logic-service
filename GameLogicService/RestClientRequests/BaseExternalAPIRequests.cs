using GameLogicService.RestClientRequests.Interfaces;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Net.Http.Headers;

namespace GameLogicService.RestClientRequests
{
    public class BaseExternalAPIRequests : IBaseExternalAPIRequests
    {
        protected readonly HttpClient _httpClient;
        protected readonly JsonSerializerOptions _options;

        public BaseExternalAPIRequests(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                PropertyNameCaseInsensitive = true
            };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
