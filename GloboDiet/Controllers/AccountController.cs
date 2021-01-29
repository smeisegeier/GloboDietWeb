using GloboDiet.Models;
using GloboDiet.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Controllers
{
    public class AccountController : Controller
    {

        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        private NavigationBar getNewNavigationBar() => new NavigationBar(0, 0, 0, 0, HelperLibrary.EfCoreHelper.SqlConnectionType.UNKNOWN);

        [HttpGet]
        public IActionResult Register()
        {
            return View(new Register(getNewNavigationBar()));
        }

        [HttpPost]
        public async Task<IActionResult> Register(Register model)
        {
            if (!ModelState.IsValid) return View();

            var user = new User { UserName = model.Username };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }

            return View();
        }


        [HttpGet]
        public IActionResult Login(string returnUrl = "")
        {
            var model = new Login(returnUrl, getNewNavigationBar());
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var result = await _signInManager.PasswordSignInAsync(viewModel.Username, viewModel.Password, viewModel.RememberMe, false);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(viewModel.ReturnUrl) && Url.IsLocalUrl(viewModel.ReturnUrl))
                {
                    return Redirect(viewModel.ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Login failed");
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
