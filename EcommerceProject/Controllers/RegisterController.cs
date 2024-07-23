using EcommerceProject.Dto;
using EcommerceProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Threading.Tasks;

namespace EcommerceProject.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(AppUserRegisterDto appUserRegisterDto)
        {
            if (!ModelState.IsValid)
            {
                return View(appUserRegisterDto);
            }

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

            try
            {
                Console.WriteLine($"User {appUser.FirstName} {appUser.LastName} created successfully.");
                var result = await _userManager.CreateAsync(appUser, appUserRegisterDto.Password);
                Console.WriteLine($"User {appUser.FirstName} {appUser.LastName} created successfully.");
                Console.WriteLine(result.Succeeded);
                if (result.Succeeded)
                {
                    // Loglama yaparak hata ayıklama
                    Console.WriteLine($"User {appUser.FirstName} {appUser.LastName} created successfully.");

                    MimeMessage mimeMessage = new MimeMessage();
                    mimeMessage.From.Add(new MailboxAddress("Eticaret Uygulaması", "alibugus@hotmail.com"));
                    mimeMessage.To.Add(new MailboxAddress("User", appUser.Email));
                    mimeMessage.Subject = "Eticaret Uygulaması";

                    BodyBuilder bodyBuilder = new BodyBuilder();
                    bodyBuilder.TextBody = $"Kaydınız Başarılı Şekilde Gerçekleşti. Kodunuz: {code}";
                    mimeMessage.Body = bodyBuilder.ToMessageBody();

                    using (var client = new SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 587, false);
                        client.Authenticate("sakabugus@gmail.com", "qmpi jaze hvhk qpqf");
                        client.Send(mimeMessage);
                        client.Disconnect(true);
                    }

                    TempData["Mail"] = appUserRegisterDto.Email;
                    return RedirectToAction("Index", "ConfirmMail");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Error: {error.Description}");
                        ModelState.AddModelError("", error.Description);
                    }

                }
            }
            catch (Exception ex)
            {
                // Hata mesajlarını model state'e ekleyin
                ModelState.AddModelError("", $"Bir hata oluştu: {ex.Message}");
            }

            return View();
        }
    }
}
