using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PermissionsAttribute.BLL.Services.AccountService;
using PermissionsAttribute.BLL.Services.ClaimService;
using PermissionsAttribute.WebUI.Models;
using PermissionsAttribute.WebUI.Models.ViewModels.Authentication;
using PermissionsAttribute.WebUI.Models.ViewModels.Errors;

namespace PermissionsAttribute.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        private readonly IClaimService _claimService;

        public AccountController(IAccountService accService, IClaimService claimService)
        {
            _accountService = accService;
            _claimService = claimService;
        }

        public ActionResult Error()
        {
            var error = new ErrorModel
            {
                Permissions = _claimService
                .GetPermissions()
                .ToList()
            };

            return View("Views/Shared/Error.cshtml", error);
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

            var convertedProfile = Utils.Convert.To<RegisterModel, BLL.Models.Authentication.RegisterModel>(model);
            var registeredProfilePermissions = await _accountService.RegisterProfileAsync(convertedProfile);

            if (registeredProfilePermissions != null)
            {
                var convertedPermission = Utils.Convert.To<BLL.Models.ProfileModels.ProfilePermission, ProfilePermission>(registeredProfilePermissions);

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

            var permission = await _accountService.LogIn(Utils.Convert.To<LoginModel, BLL.Models.Authentication.LoginModel>(model));

            if (permission != null && permission.PermissionNames.Any())
            {
                var convertedPermission = Utils.Convert.To<BLL.Models.ProfileModels.ProfilePermission, ProfilePermission>(permission);

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
            _claimService.AddPermissions(permission.PermissionNames);
            _claimService.AddId(permission.Id);
            _claimService.AddEmail(email);

            var claimsIdentity = _claimService.GetClaimsIdentity();

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }
    }
}