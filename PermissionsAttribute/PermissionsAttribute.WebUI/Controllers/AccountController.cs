using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PermissionsAttribute.BLL.Services;
using PermissionsAttribute.DAL.Models.Contexts;
using PermissionsAttribute.WebUI.Models;
using PermissionsAttribute.WebUI.Models.ViewModels;

namespace PermissionsAttribute.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IProfileService _service;

        public AccountController(IProfileService service)
        {
            _service = service;
        }

        public ActionResult Error()
        {
            var permissions = new ErrorModel();
            foreach(var claim in User.Claims)
            {
                if(claim.Type != "email" && claim.Type != "id")
                {
                    permissions.Permissions.Add(claim.Value);
                }
            }

            return View("Views/Shared/Error.cshtml", permissions);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!await _service.IsEmailExistsAsync(model.Email))
            {
                var registeredProfile = await _service.RegisterProfileAsync(Utils.Convert.To<RegisterModel, BLL.Models.RegisterModel>(model));

                var permission = await _service.GetPermissionsAsync(registeredProfile);

                var convertedPermission = Utils.Convert.To<BLL.Models.ProfilePermission, ProfilePermission>(permission);

                await Authenticate(convertedPermission, model.Email);

                return RedirectToAction("GetAllProfiles", "Profile");
            }
            else
            {
                ModelState.AddModelError("", "This Email already exists");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var permission = await _service.GetPermissionsAsync(Utils.Convert.To<LoginModel, BLL.Models.Profile>(model));

            var convertedPermission = Utils.Convert.To<BLL.Models.ProfilePermission, ProfilePermission>(permission);

            if (permission.PermissionNames.Count() != 0)
            {
                await Authenticate(convertedPermission, model.Email);

                return RedirectToAction("GetAllProfiles", "Profile");
            }
            ModelState.AddModelError("", "Incorrect Login or/and Password");

            return View(model);
        }

        public async Task<IActionResult> LogOff()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("GetAllProfiles", "Profile");
        }

        [NonAction]
        private async Task Authenticate(ProfilePermission permission, string email)
        {
            var claims = new List<Claim>();
            foreach (var permissionName in permission.PermissionNames)
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, permissionName));
            }

            claims.Add(new Claim("email", email));
            claims.Add(new Claim("id", permission.Id.ToString()));

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims,
                "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType
            );

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }
    }
}