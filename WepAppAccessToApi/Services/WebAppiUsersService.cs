using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using WepAppAccessToApi.Models;
using System.Security.Policy;

namespace WepAppAccessToApi.Services
{
    public class WebAppiUsersService : IUserService
    {

        private readonly HttpClient _httpClient;
        private const string UserUri = "api/User";

        public WebAppiUsersService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("WebAppiUsers");
        }

        public async Task<bool> Delete(int id, User model)
        {

            var request = new HttpRequestMessage(HttpMethod.Delete, $"{UserUri}/{model.Id}");

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            request.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var result = await _httpClient.SendAsync(request);

            if (!result.IsSuccessStatusCode)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> Edit(int id, User model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var result = await _httpClient.PutAsync($"{UserUri}", content);

            if (!result.IsSuccessStatusCode)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> Insert(User model)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{UserUri}");

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            request.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var result = await _httpClient.SendAsync(request);

            if (!result.IsSuccessStatusCode)
            {
                return false;
            }

            return true;
        }

        public async Task<List<User>> GetAll()
        {
            return await GetResource<List<User>>($"{UserUri}");
            //var response = await _httpClient.GetAsync(UserUri);
            //var responseBody = await response.Content.ReadAsStringAsync();

            //if (!response.IsSuccessStatusCode)
            //{
            //    return new List<User>();
            //}

            //return JsonConvert.DeserializeObject<List<User>>(responseBody);
        }

        public async Task<User> GetUserById(int id)
        {
            return await GetResource<User>($"{UserUri}/{id}");
        }

        private async Task<TReturn> GetResource<TReturn>(string url)
        {

            var result = await _httpClient.GetAsync(url);//pobiera do headera apikey ze startupu

            Log.Logger.Information($"Pobieram dane z {url}");

            if (result.StatusCode.ToString() == "Unauthorized")
            {
                Log.Logger.Error("Unauthorized!");
                throw new Exception("Blad 401");
            }

            if (!result.IsSuccessStatusCode)
            {
                Log.Logger.Error($"StatusCode : {result.StatusCode}");
                throw new Exception($"Blad ! {result.StatusCode}");//TODO how to get better view if TReturn needed
            }

            var content = await result.Content.ReadAsStringAsync();

            var deserializedResource = JsonConvert.DeserializeObject<TReturn>(content);

            return deserializedResource;
        }

    }
}
