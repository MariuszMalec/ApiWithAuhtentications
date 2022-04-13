using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using WepAppAccessToApi.Models;

namespace WepAppAccessToApi.Controllers
{
    public class UserBearerController : Controller
    {

        private readonly HttpClient _httpClient;
        private const string AppiUrl = "https://localhost:7270/api/User";
        private const string UserUri = "api/User";

        public UserBearerController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiWithBearer");
        }

        // GET: UserBearerController
        public async Task<IActionResult> Index()
        {

            var request = new HttpRequestMessage(HttpMethod.Get, $"{AppiUrl}/User");

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var result = await _httpClient.GetAsync(UserUri);

            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return Content("Unauthorized!");
            }

            if (!result.IsSuccessStatusCode)
            {
                return Content("error 500!");
            }

            var content = await result.Content.ReadAsStringAsync();

            var model = JsonConvert.DeserializeObject<List<User>>(content);
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
