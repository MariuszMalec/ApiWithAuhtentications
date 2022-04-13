using Microsoft.AspNetCore.Mvc;
using WepAppAccessToApi.Services;

namespace WepAppAccessToApi.Controllers
{
    public class WebAppiUsersController : Controller
    {
        private readonly IUserService _userService;

        public WebAppiUsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: WebAppiUsersController
        public async Task<ActionResult> Index()
        {
            var model = await _userService.GetAll();
            return View(model);
        }

        // GET: WebAppiUsersController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var model = await _userService.GetUserById(id);
            return View(model);
        }

        // GET: WebAppiUsersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WebAppiUsersController/Create
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

        // GET: WebAppiUsersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WebAppiUsersController/Edit/5
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

        // GET: WebAppiUsersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WebAppiUsersController/Delete/5
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
