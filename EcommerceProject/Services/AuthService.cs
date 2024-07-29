using EcommerceProject.Models;
using EcommerceProject.Models.Dto;
using EcommerceProject.Repositories;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EcommerceProject.Services
{
    public class AuthService
    {
        private readonly UserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(UserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        #region Login
        public async Task<Microsoft.AspNetCore.Identity.SignInResult> LoginAsync(LoginViewModel loginViewModel)
        {
            var result = await _userRepository.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, false, true);
            if (result.Succeeded)
            {
                var user = await _userRepository.FindByNameAsync(loginViewModel.UserName);
                if (user != null)
                {
                    SetUserCookies(user.FirstName, user.LastName);
                }
            }
            return result;
        }
        #endregion

        #region Logout
        public async Task LogoutAsync()
        {
            await _userRepository.SignOutAsync();
            RemoveUserCookies();
        }
        #endregion

        #region FindByName
        public async Task<AppUser> FindByNameAsync(string userName)
        {
            return await _userRepository.FindByNameAsync(userName);
        }
        #endregion

        #region RegisterUser
        public async Task<IdentityResult> RegisterUserAsync(AppUserRegisterDto appUserRegisterDto)
        {
            Random random = new Random();
            int code = random.Next(10000, 1000000);

            AppUser appUser = new AppUser()
            {
                FirstName = appUserRegisterDto.FirstName,
                LastName = appUserRegisterDto.LastName,
                City = appUserRegisterDto.City,
                UserName = appUserRegisterDto.UserName,
                Email = appUserRegisterDto.Email,
                PhoneNumber = appUserRegisterDto.PhoneNumber,
                ConfirmCode = code,
            };

            var result = await _userRepository.CreateAsync(appUser, appUserRegisterDto.Password);
            if (result.Succeeded)
            {
                await SendConfirmationEmailAsync(appUser.Email, code);
            }

            return result ?? IdentityResult.Failed(new IdentityError { Description = "Unknown error occurred." });
        }
        #endregion

        #region SendConfirmationEmail
        private async Task SendConfirmationEmailAsync(string email, int code)
        {
            MimeMessage mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("E-commerce Application", "alibugus@hotmail.com"));
            mimeMessage.To.Add(new MailboxAddress("User", email));
            mimeMessage.Subject = "E-commerce Application";

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = $"Your registration was successful. Your code: {code}";
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("sakabugus@gmail.com", "qmpi jaze hvhk qpqf");
                client.Send(mimeMessage);
                client.Disconnect(true);
            }
        }
        #endregion

        #region PasswordReset
        public async Task<string> GeneratePasswordResetTokenLinkAsync(string email, IUrlHelper urlHelper, HttpRequest request)
        {
            var user = await _userRepository.FindByEmailAsync(email);
            if (user != null)
            {
                var token = await _userRepository.GeneratePasswordResetTokenAsync(user);
                var passwordResetTokenLink = urlHelper.Action("ResetPassword", "PasswordChange", new
                {
                    userId = user.Id,
                    token = token
                }, request.Scheme);

                return passwordResetTokenLink;
            }
            return null;
        }

        public async Task SendPasswordResetEmailAsync(string email, string passwordResetTokenLink)
        {
            MimeMessage mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("E-commerce Application", "sakabugus@gmail.com"));
            mimeMessage.To.Add(new MailboxAddress("User", email));
            mimeMessage.Subject = "E-commerce Application";

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = passwordResetTokenLink;
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("sakabugus@gmail.com", "qmpi jaze hvhk qpqf");
                client.Send(mimeMessage);
                client.Disconnect(true);
            }
        }

        public async Task<IdentityResult> ResetPasswordAsync(string userId, string token, string newPassword)
        {
            var user = await _userRepository.FindByIdAsync(userId);
            if (user != null)
            {
                return await _userRepository.ResetPasswordAsync(user, token, newPassword);
            }
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });
        }
        #endregion

        #region CookieMethods
        public void SetUserCookies(string firstName, string lastName)
        {
            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                HttpOnly = true,
                Secure = true
            };

            _httpContextAccessor.HttpContext.Response.Cookies.Append("FirstName", firstName, options);
            _httpContextAccessor.HttpContext.Response.Cookies.Append("LastName", lastName, options);
        }

        public void RemoveUserCookies()
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("FirstName");
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("LastName");
        }
        #endregion
    }
}
