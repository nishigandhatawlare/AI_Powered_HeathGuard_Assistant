using Health_Guard_Assistant.Web.Services;
using Health_Guard_Assistant.Web.Services.IServices;
using Health_Guard_Assistant.Web.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IAppointmentService,AppointmentService>();
builder.Services.AddHttpClient<IProviderService, ProviderService>();
builder.Services.AddHttpClient<ILocationService, LocationService>();
builder.Services.AddHttpClient<ISpecialityService, SpecialityService>();

SD.AppointmentApiBase = builder.Configuration["ServiceUrls:AppointmentApi"];
builder.Services.AddScoped<IBaseService,BaseServices>();
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
