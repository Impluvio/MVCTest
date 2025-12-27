using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using MVCtest.Models;
using MVCtest.Service;
using System.Security.Cryptography.Xml;

namespace MVCtest.Controllers
{
    public class IdentityController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender emailSender;

        public IdentityController(UserManager<IdentityUser> userManager, IEmailSender emailSender)
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
                    user = await _userManager.FindByEmailAsync(model.Email);
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    
                    if (result.Succeeded)
                    {
                        var confirmationLink = Url.ActionLink("ConfirmEmail", "Identity", new { userId = user.Id, token });
                        await emailSender.SendEmailAsync("adamt@mydomain.co.uk", user.Email, "Confirm your Emails Address", confirmationLink);
                        
                        return RedirectToAction("Signin");
                    }

                    ModelState.AddModelError("Signup", string.Join(", ", result.Errors.Select(x => x.Description)));

                }
            
            }


            return View(model);
        }

        public async Task<IActionResult> ConfirmEmail(string userId,string token)
        {
           var user = await _userManager.FindByNameAsync(userId);

           var emailConfirmedResult = await _userManager.ConfirmEmailAsync(user, token);

            if (emailConfirmedResult.Succeeded) 
            {
                return new OkResult();

            }

            return new NotFoundResult();    
            
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
