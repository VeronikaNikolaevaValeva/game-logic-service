using GameLogicService.RestClientRequests.Interfaces;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Net.Http.Headers;
using GameLogicService.Models.Responses;
using Azure;
using RestSharp;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Azure.Core;
using System.Net.Http;
using System;
using Auth0.ManagementApi;

namespace GameLogicService.RestClientRequests
{
    public class AuthAPIRequests : IAuthAPIRequests
    {
        protected readonly HttpClient _httpClient;
        protected readonly JsonSerializerOptions _options;

        public AuthAPIRequests(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri($"https://dev-he67eqpc846lev05.us.auth0.com/api/v2/users");
            _options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                PropertyNameCaseInsensitive = true
            };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
        }
        public async Task<bool> DeleteAuthUserData(string authId, string token)
        {
            try
            {

                var managementApi = new ManagementApiClient(token, new Uri("https://dev-he67eqpc846lev05.us.auth0.com/"), new HttpClientManagementConnection());
                await managementApi.Users.DeleteAsync(authId);
                using (var client = new HttpClient())
                {
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer ", token);
                    //client.BaseAddress = new Uri("https://dev-he67eqpc846lev05.us.auth0.com");
                    //var result = await client.DeleteAsync("/api/v2/users/" + authId);
                    //string resultContent = await result.Content.ReadAsStringAsync();
                    //var response = JObject.Parse(resultContent);
                    //Console.WriteLine(result.StatusCode.ToString());
                    //Console.WriteLine(result.Content.ToString());
                    //Console.WriteLine(resultContent.ToString());
                    //Console.WriteLine(response.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            return true;
        }
    }
}
