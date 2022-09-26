using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using WepAppAccessToApi.Models;

namespace WepAppAccessToApi.Controllers
{
    public class UserBearerController : Controller
    {

        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private const string AppiUrl = "https://localhost:7270/api/User";
        private const string UserUri = "api/User";
        private readonly IMapper _mapper;

        public UserBearerController(IHttpClientFactory httpClientFactory, IMapper mapper = null)
        {
            //_httpClient = httpClientFactory.CreateClient("ApiWithBearer");
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
        }

        // GET: UserBearerController
        public async Task<IActionResult> Index()//Nie dzial add view tutaj!
        {

            var client = _httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, $"{AppiUrl}");

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //TODO jak dodac ten token do zapytania?
            //zalogowac sie w ApiWithAuhtenticationBearer, otrzymany token ktory skopiowac nizej
            //i odpalic to Web app, dziala 240min, czas zapisany w patrz aspsettings.json
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjMiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiTWFyaXVzeiBNYWxlYyIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6ImFkbWluIiwiRGF0ZU9mQmlydGgiOiIyMDE0LTA1LTAxIiwiTmF0aW9uYWxpdHkiOlsicG9saXNoIiwicG9saXNoIl0sImV4cCI6MTY2NDIyMzMxNSwiaXNzIjoiaHR0cDovL0FwaVdpdGhBdWh0ZW50aWNhdGlvbkJlYXJlci5jb20iLCJhdWQiOiJodHRwOi8vQXBpV2l0aEF1aHRlbnRpY2F0aW9uQmVhcmVyLmNvbSJ9.8_3Y3JcwqEO92lXNk0ODj09k02wP5Q6z13oAlW7gm10";

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var result = await client.SendAsync(request);

            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return Content("Unauthorized!");
            }

            if (!result.IsSuccessStatusCode)
            {
                return Content("error 500!");
            }

            var content = await result.Content.ReadAsStringAsync();

            var model = JsonConvert.DeserializeObject<List<UserGet>>(content);

            //var dtos = _mapper.Map<List<UserDto>>(model);//TODO automatyczne mapowanie nie dziala, poprawic w profile

            var models = model.Select(MapUserGetToUserModel);//reczne mapowanie dzila

            return View(models);
        }

        private UserDto MapUserGetToUserModel(UserGet get)
        {
            return new UserDto
            {
                Id = get.Id,
                Email = get.Email,
                FirstName = get.FirstName,
                LastName = get.LastName,
                DateOfBirth = get.DateOfBirth,
                Nationality = get.Nationality,
                PasswordHash = "***",
                RoleId = get.RoleId,
                Role = new Role ()
                {
                    Id=get.Role.Id,
                    Name=get.Role.Name
                }
            };
        }

        public async Task<IActionResult> GetRolles()//TODO dodac View
        {

            //var request = new HttpRequestMessage(HttpMethod.Get, $"{AppiUrl}/Roles");

            //request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var result = await _httpClient.GetAsync($"{UserUri}/Roles");

            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return Content("error 401, Unauthorized");
            }

            if (!result.IsSuccessStatusCode)
            {
                return Content("error 500!");
            }

            var content = await result.Content.ReadAsStringAsync();

            var model = JsonConvert.DeserializeObject<List<RoleDto>>(content);
            return View(model);
        }

        // GET: UserBearerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserBearerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserBearerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserBearerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserBearerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserBearerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserBearerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
