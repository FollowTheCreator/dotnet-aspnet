using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PermissionsAttribute.BLL.Services.AccountService;
using PermissionsAttribute.WebUI.Models;
using PermissionsAttribute.WebUI.Models.ViewModels.Authentication;
using PermissionsAttribute.WebUI.Models.ViewModels.Errors;
using Utils;

namespace PermissionsAttribute.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService accService)
        {
            _service = accService;
        }

        public ActionResult Error()
        {
            var permissions = new ErrorModel();
            foreach(var claim in User.Claims)
            {
                if(claim.Type == ClaimsIdentity.DefaultRoleClaimType)
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

            var convertedProfile = Utils.Convert.To<RegisterModel, BLL.Models.RegisterModel>(model);
            var registeredProfilePermissions = await _service.RegisterProfileAsync(convertedProfile);

            if (registeredProfilePermissions != null)
            {
                var convertedPermission = Utils.Convert.To<BLL.Models.ProfilePermission, ProfilePermission>(registeredProfilePermissions);

                await Authenticate(convertedPermission, model.Email);

                return RedirectToAction("GetAllProfiles", "Profile");
            }

            ModelState.AddModelError("", "This Email already exists");
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

            var permission = await _service.LogIn(Utils.Convert.To<LoginModel, BLL.Models.Profile>(model));

            var convertedPermission = Utils.Convert.To<BLL.Models.ProfilePermission, ProfilePermission>(permission);

            if (convertedPermission != null && convertedPermission.PermissionNames.Any())
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