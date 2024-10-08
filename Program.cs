using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SportSync.Data.Servicios;
using SportSync.Data;
using SportSync.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SportsyncContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar servicios de autenticaci�n
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Home/Login";
        options.LogoutPath = "/Home/Logout";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });

builder.Services.AddSingleton(new Contexto(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=PageMain}/{action=Inicio}/{id?}");

app.Run();
