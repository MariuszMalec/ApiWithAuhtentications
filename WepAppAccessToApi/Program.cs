using Microsoft.Net.Http.Headers;
using WepAppAccessToApi.Services;

var builder = WebApplication.CreateBuilder(args);

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
