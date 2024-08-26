
using EcommerceProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace EcommerceProject.Controllers
{
    public class GoogleSignInController : Controller
    {
        private readonly GoogleSignInService _googleSignInService;

        public GoogleSignInController(GoogleSignInService googleSignInService)
        {
            _googleSignInService = googleSignInService;
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult GoogleLogin()
        {
            var properties = _googleSignInService.ConfigureExternalAuthenticationProperties();
            return new ChallengeResult("Google", properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult> GoogleResponse()
        {
            return await _googleSignInService.HandleGoogleResponse();
        }
    }
    }





