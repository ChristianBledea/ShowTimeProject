using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShowTime.Businesslogic.Abstractions;
using ShowTime.Businesslogic.Dtos;
using ShowTime.Businesslogic.Services;
using ShowTime.BusinessLogic.Abstractions;
using ShowTime.BusinessLogic.Services;
using ShowTime.Client.Pages;
using ShowTime.Components;
using ShowTime.DataAccess;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.Repositories;
using ShowTime.DataAccess.Repositories.Abstractions;
using ShowTime.DataAccess.Repositories.Implementations;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ShowTimeContext");
builder.Services.AddDbContext<ShowtimeDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "auth-token";
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/access-denied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });

builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

// Add Identity services for password hashing
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();


builder.Services.AddTransient<ITicketService, TicketService>();
builder.Services.AddTransient<ITicketRepository, TicketRepository>();
builder.Services.AddTransient<IBookingRepository, BookingRepository>();
builder.Services.AddTransient<IBookingService, BookingService>();
builder.Services.AddTransient<ILineupRepository, LineupRepository>();
builder.Services.AddTransient<ILineupService, LineupService>();
builder.Services.AddTransient<IArtistRepository, ArtistRepository>();
builder.Services.AddTransient<IFestivalRepository, FestivalRepository>();
builder.Services.AddTransient<IRepository<Artist>, GenericRepository<Artist>>();
builder.Services.AddTransient<IArtistService, ArtistService>();
builder.Services.AddTransient<IRepository<Festival>, GenericRepository<Festival>>();
builder.Services.AddTransient<IFestivalService, FestivalService>();
builder.Services.AddTransient<IRepository<User>, GenericRepository<User>>();
builder.Services.AddTransient<IUserRepository, UserRepository>(); // Add this line
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Add authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(ShowTime.Client._Imports).Assembly);

app.Run();