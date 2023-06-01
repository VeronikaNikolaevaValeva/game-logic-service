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
        public async Task<bool> DeleteAuthUserData(string authId, string tokeen)
        {
            try
            {
                //var client = new RestClient("https://dev-he67eqpc846lev05.us.auth0.com/oauth/token");
                //var request = new RestRequest(Method.Post.ToString());
                //request.AddHeader("content-type", "application/json");
                //request.AddParameter("application/json", "{\"client_id\":\"5dVfPMbskP7LMi0nIXtVou5KUdxoxnSQ\",\"client_secret\":\"Uqz-umF1JmKWukJ9OY9b45vTKG5bCpgy9C7vtSAt40N-LZccZzTkpdIJzpao3KC1\",\"audience\":\"https://dev-he67eqpc846lev05.us.auth0.com/api/v2/\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);
                //var response = client.Execute(request);
                //Console.WriteLine(response.StatusCode.ToString());
                //Console.WriteLine(response.Content.ToString());

                var token = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IjFyVUcybjNsTFZ2cmhWMGRhZG01cSJ9.eyJpc3MiOiJodHRwczovL2Rldi1oZTY3ZXFwYzg0NmxldjA1LnVzLmF1dGgwLmNvbS8iLCJzdWIiOiI1ZFZmUE1ic2tQN0xNaTBuSVh0Vm91NUtVZHhveG5TUUBjbGllbnRzIiwiYXVkIjoiaHR0cHM6Ly9kZXYtaGU2N2VxcGM4NDZsZXYwNS51cy5hdXRoMC5jb20vYXBpL3YyLyIsImlhdCI6MTY4NTYyODc1MCwiZXhwIjoxNjg1NzE1MTUwLCJhenAiOiI1ZFZmUE1ic2tQN0xNaTBuSVh0Vm91NUtVZHhveG5TUSIsInNjb3BlIjoiZGVsZXRlOnVzZXJzIiwiZ3R5IjoiY2xpZW50LWNyZWRlbnRpYWxzIn0.Q2BD6zH503pu_p55brpP7GOJPxcQDz5VEC_0UZuSgDTda-05LfPGA7WeVdAYHTCXVHycFx6W-C14oLUhhYZBxGp4nXNsH3IV8BdYHIoFT1MRwJy2pTLO-t6u6n8ulhiP-rloRuxEnin8lAU0l-EA486BeCKGCBNx6IlUuwJ-lr_CQlwHeoQzCtW0ug-2D563a7I7NnB5fzgqeLqp2ZWfv6t1uMjoIJ93qP0OxFShM7lnAchHKoNZvpsA3R1SN8AYFd0VU8M_sDk-5arU6uERxB8eOKVN8UflHeNZAtjlyxD-We85pSgZ92bbc8gmWDR0NJTFdxC1W0OAO8jSur6-zw";
                var managementApi = new ManagementApiClient(token, new Uri("https://dev-he67eqpc846lev05.us.auth0.com/"), new HttpClientManagementConnection());
                await managementApi.Users.DeleteAsync(authId);
                //using (var client = new HttpClient())
                //{
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer ", token);
                //client.BaseAddress = new Uri("https://dev-he67eqpc846lev05.us.auth0.com");
                //var result = await client.DeleteAsync("/api/v2/users/" + authId);
                //string resultContent = await result.Content.ReadAsStringAsync();
                //var response = JObject.Parse(resultContent);
                //Console.WriteLine(result.StatusCode.ToString());
                //Console.WriteLine(result.Content.ToString());
                //Console.WriteLine(resultContent.ToString());
                //Console.WriteLine(response.ToString());
                //}
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
