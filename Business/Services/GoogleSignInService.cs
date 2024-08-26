using Data.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Data.Services
{
    public class GoogleSignInService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly AuthService _authService;

        public GoogleSignInService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, AuthService authService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _authService = authService;
        }


        public async Task<ExternalLoginInfo> GetExternalLoginInfoAsync()
        {
            return await _signInManager.GetExternalLoginInfoAsync();
        }

        public async Task<Microsoft.AspNetCore.Identity.SignInResult> ExternalLoginSignInAsync(string provider, string providerKey, bool isPersistent)
        {
            return await _signInManager.ExternalLoginSignInAsync(provider, providerKey, isPersistent);
        }

        public async Task<IdentityResult> CreateUserAsync(AppUser user, ExternalLoginInfo info)
        {
            IdentityResult identResult = await _userManager.CreateAsync(user);
            if (identResult.Succeeded)
            {
                identResult = await _userManager.AddLoginAsync(user, info);
            }
            return identResult;
        }

        public async Task SignInUserAsync(AppUser user, bool isPersistent)
        {
            await _signInManager.SignInAsync(user, isPersistent);
        }


        public AuthenticationProperties ConfigureExternalAuthenticationProperties()
        {
            string redirectUrl = "/GoogleSignIn/GoogleResponse";
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            properties.Items[CookieAuthenticationDefaults.AuthenticationScheme] = "select_account";
            return properties;
        }

        public async Task<IActionResult> HandleGoogleResponse()
        {
            ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return new RedirectToActionResult("Login", "Account", null);

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(info.Principal.FindFirst(ClaimTypes.Email)?.Value);
                if (user != null)
                {
                    _authService.SetUserCookies(user.FirstName, user.LastName);
                    return new RedirectToActionResult("Index", "Home", null);
                }
                return new RedirectToActionResult("AccessDenied", "GoogleSignIn", null);
            }
            else
            {
                var email = info.Principal.FindFirst(ClaimTypes.Email)?.Value;
                var firstName = info.Principal.FindFirst(ClaimTypes.GivenName)?.Value ?? "DefaultFirstName";
                var lastName = info.Principal.FindFirst(ClaimTypes.Surname)?.Value ?? "DefaultLastName";

                var user = new AppUser
                {
                    Email = email,
                    UserName = email,
                    FirstName = firstName,
                    LastName = lastName,
                    City = "City",
                    ConfirmCode = 1
                };

                var identResult = await _userManager.CreateAsync(user);
                if (identResult.Succeeded)
                {
                    identResult = await _userManager.AddLoginAsync(user, info);
                    if (identResult.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false);
                        _authService.SetUserCookies(user.FirstName, user.LastName);
                        return new RedirectToActionResult("Index", "Home", null);
                    }
                }
                return new RedirectToActionResult("AccessDenied", "GoogleSignIn", null);
            }
        }
    }
}
