using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using MVCtest.Models;

namespace MVCtest.Controllers
{
    public class IdentityController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;

        public IdentityController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> signUp()
        {
            var model = new SignupModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> signUp(SignupModel model)
        {
            if (ModelState.IsValid) 
            { 
                if((await _userManager.FindByEmailAsync(model.Email)) != null) 
                {
                    var user = new IdentityUser
                    {
                        Email = model.Email,
                        UserName = model.Email
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Signin");
                    }

                    ModelState.AddModelError("Signup", string.Join(", ", result.Errors.Select(x => x.Description)));

                }
            
            }


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
