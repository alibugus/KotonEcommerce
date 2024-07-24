using EcommerceProject.Database;
using EcommerceProject.Models;
using EcommerceProject.Repositories;
using EcommerceProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Register repositories
builder.Services.AddScoped<UserRepository>();

// Register services
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ConfirmMailService>();

// Configure Google Authentication
builder.Services.AddAuthentication()
        .AddGoogle(opts =>
        {
            opts.ClientId = "116794396854-alhpnf8vtjk6hk1ges7mov2o80dfan7m.apps.googleusercontent.com";
            opts.ClientSecret = "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";
            opts.SignInScheme = IdentityConstants.ExternalScheme;
        });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Add this line
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
