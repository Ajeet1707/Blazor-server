using Blazored.Toast;
using BlazorServerApp.Data;
using BlazorServerApp.Data.Configuration;
using BlazorServerApp.Data.CustomAuthorization;
using BlazorServerApp.Data.Infrastructure;
using BlazorServerApp.Data.Service;
using BlazorServerApp.Data.SIgnalRComm;
using MatBlazor;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using System.Globalization;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<IDemoService,DemoService>();

// Register IHttpContextAccessor and CookieService
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<CookieService>();

//This is the code to add to get the HTTP call for the project.
builder.Services.AddSingleton<ImportDataService>();
builder.Services.AddHttpClient<IImportData, ImportDataService>(
	client =>
	{
		client.BaseAddress = new Uri("https://localhost:7267/");
	});
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddServerSideBlazor()
   .AddCircuitOptions(options => { options.DetailedErrors = true; });


//New Authorization
builder.Services.AddScoped<BlazorSchoolUserService>();
builder.Services.AddScoped<BlazorSchoolAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp
    => sp.GetRequiredService<BlazorSchoolAuthenticationStateProvider>());

//Adding authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(option => {
		option.Cookie.Name = "auth_token";
		option.LoginPath = "/login";
        option.Cookie.MaxAge = TimeSpan.FromMinutes(30);
		option.AccessDeniedPath = "/access-denied";
	});
builder.Services.AddAuthorization();

//Adding a toaster
builder.Services.AddBlazoredToast()
	.AddBlazorBootstrap();
builder.Services.AddMatBlazor();

//SignalR is used for real-time notification
builder.Services.AddSignalR();

//Localization
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddControllers();

builder.Services.AddAuthorizationCore();



builder.Services.AddHttpClient();
builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

//Calling customConfiguration
var customConfig = new CustomConfiguration();
//adding localization middleware
customConfig.Configure(app, app.Environment);
app.MapControllers();

//Adding authorization and authentication middleware
app.UseAuthentication()
	.UseAuthorization();

//SignalR
app.MapHub<ChatHub>("/chathub");



app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
