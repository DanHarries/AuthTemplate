using AuthTemplate.Data;
using AuthTemplate.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthTemplate.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    { 
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<MyApplicationUser> _userManager;
        private readonly SignInManager<MyApplicationUser> _signInManager;       

        public AccountController(
            UserManager<MyApplicationUser> userManager, 
            SignInManager<MyApplicationUser> signInManager, 
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var user = new MyApplicationUser
                    {
                        UserName = model.Username,
                        Email = model.Email,
                    };

                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                        var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token }, Request.Scheme);

                        // Send Email 
                        //await _sendEmail.SendAccountEmail(user, confirmationLink, "Confirm", "Confirm Email");

                        ViewBag.ErrorTitle = "Registration successful";
                        ViewBag.ErrorMessage = "<p>Please <strong>confirm</strong> your email, by clicking on the confirmation link we have emailed you.</p>";

                        return View("Confirm");

                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                }

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError($"There was an issue registering user, {ex.Message}");
                throw ex;
            }

        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email: {email} is already in use");
            }
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> IsUserNameInUse(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Username: {username} is already in use");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }



    }
}
