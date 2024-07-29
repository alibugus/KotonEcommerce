using EcommerceProject.Models;
using Microsoft.AspNetCore.Identity;

namespace EcommerceProject.Services
{
    public class GoogleSignInService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public GoogleSignInService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<ExternalLoginInfo> GetExternalLoginInfoAsync()
        {
            return await _signInManager.GetExternalLoginInfoAsync();
        }

        public async Task<SignInResult> ExternalLoginSignInAsync(string provider, string providerKey, bool isPersistent)
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
    }
}
