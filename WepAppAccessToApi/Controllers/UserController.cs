using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using WepAppAccessToApi.Models;

namespace WepAppAccessToApi.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        private const string AppiUrl = "https://localhost:7172/api";

        public UserController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        // GET: UserController
        public async Task<IActionResult> Index()
        {
            var client = httpClientFactory.CreateClient();

            //AuthenticationHeaderValue("Bearer", "fa4c82d5b75e4cd351b1ea519c9dfd8312dea97c43f0aa13012d4ff2fd109763");
            //AuthenticationHeaderValue("Bearer", $"{token}");
            //AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiKey", "8e421ff965cb4935ba56ef7833bf4750");

            var request = new HttpRequestMessage(HttpMethod.Get, $"{AppiUrl}/User");

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            request.Headers.Add("ApiKey", "8e421ff965cb4935ba56ef7833bf4750");

            var result = await client.SendAsync(request);

            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return Content("Unauthorized!");
            }

            var content = await result.Content.ReadAsStringAsync();

            var model = JsonConvert.DeserializeObject<List<User>>(content);
            return View(model);
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
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

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
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

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
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
