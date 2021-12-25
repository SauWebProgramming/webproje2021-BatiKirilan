using manav.Models;
using manav.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace manav.Controllers
{
    public class UserController : Controller
    {
        public readonly UserManager<AppUser> _userManager;
        public readonly SignInManager<AppUser> _signInManager;
        public readonly RoleManager<AppRole> _roleManager;

        public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginDTO dto)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByEmailAsync(dto.Email);
                if (user != null)
                {
                    SignInResult result = await _signInManager.PasswordSignInAsync(user, dto.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("LoginErrors", "Geçersiz mail adresi veya şifre");
                    }
                }
            }
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserDTO user)
        {
            if (ModelState.IsValid)
            {
                if (user.Password == user.ConfirmPassword)
                {
                    AppUser newUser = new AppUser();
                    newUser.UserName = user.UserName;
                    newUser.Email = user.Email;
                    newUser.PhoneNumber = user.PhoneNumber;
                    newUser.Name = user.Name;
                    newUser.SurName = user.SurName;
                    IdentityResult result = await _userManager.CreateAsync(newUser, user.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(newUser, "User");
                        return RedirectToAction("SignIn");
                    }
                    else
                    {
                        foreach (IdentityError item in result.Errors)
                        {
                            ModelState.AddModelError("SignUpErrors", item.Description);
                        }
                    }
                }
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> CreateRole(string role)
        {
            
            IdentityResult result = await _roleManager.CreateAsync(new AppRole { Name = role });
            if (result.Succeeded)
            {
                return RedirectToAction("SignIn");
            }
            return RedirectToAction("SignIn");
        }
    }
}
