using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Serilog;
using WepAppAccessToApi.Areas.Identity.Data;
using WepAppAccessToApi.Data;
using WepAppAccessToApi.Middleware;
using WepAppAccessToApi.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("WepAppAccessToApiContextConnection");builder.Services.AddDbContext<WepAppAccessToApiContext>(options =>
    options.UseSqlite(connectionString));builder.Services.AddDefaultIdentity<WepAppAccessToApiUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<WepAppAccessToApiContext>();


var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets<Program>(optional: true)
    .AddEnvironmentVariables()
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

Log.Logger.Information("Start Application ...");

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IUserService , WebAppiUsersService>();

//Report API Http strzelanie do WebAppiUsers.Api
builder.Services.AddHttpClient("WebAppiUsers", client =>
{
    client.BaseAddress = new Uri("https://localhost:7024/");
    client.Timeout = new TimeSpan(0, 0, 30);
    client.DefaultRequestHeaders.Add(
        HeaderNames.Accept, "application/json");
    client.DefaultRequestHeaders.Add("ApiKey", "8e421ff965cb4935ba56ef7833bf4750");//TODO Apikey do headera nie dziala
});

builder.Services.AddHttpClient("ApiWithBearer", client =>
{
    client.BaseAddress = new Uri("https://localhost:7270/");
    client.Timeout = new TimeSpan(0, 0, 30);
    client.DefaultRequestHeaders.Add(
        HeaderNames.Accept, "application/json");
});

builder.Services.AddHttpClient("WebAppiUsersWithApiKey", client =>
{
    client.BaseAddress = new Uri("https://localhost:7172/");
    client.Timeout = new TimeSpan(0, 0, 30);
    client.DefaultRequestHeaders.Add(
        HeaderNames.Accept, "application/json");
});

builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


//TODO to dodalem aby dziala z automatu register i logon
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});



app.Run();
