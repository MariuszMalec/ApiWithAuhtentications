using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WepAppAccessToApi.Models;
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

        // GET: TrainerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserControlle/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(User model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var check = await _userService.Insert(model);

                if (check == false)
                {
                    return BadRequest("User was not created!");
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public async Task<ActionResult<User>> Edit(int id)
        {
            var model = await _userService.GetUserById(id);
            if (model == null)
            {
                return RedirectToAction("EmptyList");
            }
            return View(model);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, User model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var check = await _userService.Edit(id, model);

                if (check == false)
                {
                    return RedirectToAction(nameof(Index));
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WebAppiUsersController/Delete/5
        // GET: UserController/Delete/5
        [Authorize]
        public async Task<ActionResult<User>> Delete(int id)
        {
            var model = await _userService.GetUserById(id);
            if (model == null)
            {
                return RedirectToAction("EmptyList");
            }
            return View(model);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, User model)
        {
            try
            {
                var check = await _userService.Delete(id, model);

                if (check == false)
                {
                    return RedirectToAction("EmptyList");
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
