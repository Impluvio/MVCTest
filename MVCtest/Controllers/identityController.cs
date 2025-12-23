using Microsoft.AspNetCore.Mvc;
using MVCtest.Models;

namespace MVCtest.Controllers
{
    public class identityController : Controller
    {

        public async Task<IActionResult> signUp()
        {
            var model = new SignupModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> signUp(SignupModel model)
        {
            return View(model);
        }

        public async Task<IActionResult> signIn()
        {
            return View();
        }

        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }

    }
}
