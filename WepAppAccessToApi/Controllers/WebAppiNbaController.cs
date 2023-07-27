using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Dynamic;
using System.Net.Http.Headers;
using System.Text;
using WepAppAccessToApi.Models;

namespace WepAppAccessToApi.Controllers
{
    public class WebAppiNbaController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        private const string AppiUrl = "https://localhost:5001/api";

        public WebAppiNbaController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        // GET: UserController
        public async Task<IActionResult> Index()
        {
            var client = httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, $"{AppiUrl}/Users");

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var result = await client.SendAsync(request);

            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return Content("Unauthorized!");
            }

            var content = await result.Content.ReadAsStringAsync();

            var model = JsonConvert.DeserializeObject<List<UserFromNbaApii>>(content);
            return View(model);
        }

        public async Task<ActionResult> TestAjax()
        {
            var client = httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, $"{AppiUrl}/Users");

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var result = await client.SendAsync(request);

            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return Content("Unauthorized!");
            }

            var content = await result.Content.ReadAsStringAsync();

            var model = JsonConvert.DeserializeObject<List<UserFromNbaApii>>(content);
            return Ok(content);
        }


        public ActionResult GetAllUsersWithAuthenticate()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> GetAllUsersWithAuthenticate(AuthenticateModel model)//pusty model
        {
            var client = httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Post, $"{AppiUrl}/Users/authenticateAll");

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            request.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var result = await client.SendAsync(request);

            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return Content("Unauthorized!");
            }

            var content = await result.Content.ReadAsStringAsync();

            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return Content($"Bad Request! {content}");
            }

            var viewModel = JsonConvert.DeserializeObject<List<UserFromNbaApii>>(content);

            //return Content($"Result get from Api {content}");

            if (viewModel != null)
            {
                //trzeba serilize aby poszlo pomiedzy modelami albo poslac od razu content
                TempData["UserFromNbaApii"] = JsonConvert.SerializeObject(viewModel);
                return RedirectToAction(nameof(ViewUsersFromApi));
            }

            return View();
        }
        public ActionResult ViewUsersFromApi()
        {
            var viewModel = JsonConvert.DeserializeObject<List<UserFromNbaApii>>((string)TempData["UserFromNbaApii"]);

            ViewBag.Users = TempData["UserFromNbaApii"];

            return View(viewModel);
        }



        // GET: WebAppiNbaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WebAppiNbaController/Edit/5
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

        // GET: WebAppiNbaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WebAppiNbaController/Delete/5
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
