using Newtonsoft.Json;
using WepAppAccessToApi.Models;

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

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("Blad 500!");
            }

            var content = await result.Content.ReadAsStringAsync();

            var deserializedResource = JsonConvert.DeserializeObject<TReturn>(content);

            return deserializedResource;
        }

    }
}
