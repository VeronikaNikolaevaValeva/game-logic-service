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
using Auth0.ManagementApi.Models;

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
        public async Task<bool> DeleteAuthUserData(string authId, string tokeen)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new System.Uri("https://dev-he67eqpc846lev05.us.auth0.com/");
                    var response = await client.PostAsync("oauth/token", new FormUrlEncodedContent(
                        new Dictionary<string, string>
                        {
                            { "grant_type", "client_credentials" },
                            { "client_id", "748D2GkhkzSpr5F2Yi7DaaKSM0bgJeGk" },
                            { "client_secret", "giTal7sdaGZSgwsgAwCvSgWFlyFtGHd9uZqP8OyALdkiFCjoIB6mhMt_y1VR5dnr" },
                            { "audience", "https://dev-he67eqpc846lev05.us.auth0.com/api/v2/" }
                        }
                    ));

                    var content = await response.Content.ReadAsStringAsync();
                    var jsonResult = JObject.Parse(content);

                    var mgmtToken = jsonResult["access_token"].Value<string>();
                    Console.WriteLine(response.StatusCode.ToString());
                    Console.WriteLine(jsonResult.ToString());
                    Console.WriteLine(mgmtToken);
                    using (var mgmtClient = new ManagementApiClient(mgmtToken, new System.Uri("https://dev-he67eqpc846lev05.us.auth0.com/api/v2")))
                    {
                        await mgmtClient.Users.DeleteAsync(authId);
                    }
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
