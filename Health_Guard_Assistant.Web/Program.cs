using Health_Guard_Assistant.Web.Services;
using Health_Guard_Assistant.Web.Services.IServices;
using Health_Guard_Assistant.Web.Utility;
using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;
using Serilog.Events;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Default expiration
        options.SlidingExpiration = true; // Cookie will be refreshed if nearing expiration
    });
builder.Services.AddAuthorization();

// Setup Serilog with multiple sinks (File, Console, and Seq for cloud logging)
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()  // Set minimum log level to Information
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)  // Filter out noisy logs
    .Enrich.FromLogContext()  // Include context data in logs
    .Enrich.WithThreadId()     // Adds thread ID
    .Enrich.WithMachineName()  // Adds machine name
    .Enrich.WithEnvironmentName() // Add environment (e.g., Development, Production)
    .WriteTo.Console() // Log to console for quick debugging
    .WriteTo.File(
        path: "logs/log-.txt",  // Write to rolling file logs
        rollingInterval: RollingInterval.Day,  // Roll logs daily
        retainedFileCountLimit: 7,  // Keep only the last 7 days of logs
        fileSizeLimitBytes: 10_000_000,  // Limit log file size to 10MB
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}")  // Customize log format
    .WriteTo.Seq("http://localhost:5341")  // Optional: Send logs to Seq server (cloud/remote logging)
    .CreateLogger();

// Use Serilog as the logging provider
builder.Host.UseSerilog();


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IAppointmentService,AppointmentService>();
builder.Services.AddHttpClient<IProviderService, ProviderService>();
builder.Services.AddHttpClient<ILocationService, LocationService>();
builder.Services.AddHttpClient<ISpecialityService, SpecialityService>();
builder.Services.AddHttpClient<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();


SD.AppointmentApiBase = builder.Configuration["ServiceUrls:AppointmentApi"];
SD.AuthApiBase = builder.Configuration["ServiceUrls:AuthApi"];

builder.Services.AddScoped<IBaseService,BaseServices>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IProviderService, ProviderService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<ISpecialityService, SpecialityService>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
// Ensure the logger is properly flushed when the application shuts down
Log.CloseAndFlush();
