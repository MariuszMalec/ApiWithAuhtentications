using ApiWithAuhtenticationBearer.Entities;
using ApiWithAuhtenticationBearer.Interfaces;
using ApiWithAuhtenticationBearer.Models;
using ApiWithAuhtenticationBearer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var authenticationSettings = new AutenticationSettings();
ConfigurationManager configuration = builder.Configuration;
configuration.GetSection("Autentication").Bind(authenticationSettings);

builder.Services.AddSingleton(authenticationSettings);

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
    };
});
//https://youtu.be/exKLvxaPI6Y?t=3232

builder.Services.AddControllers();

builder.Services.AddTransient<UserService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IAccountService, AccountService>();

//nie dziala!
//builder.Services.AddHttpClient("ApiWithBearer", httpClient =>
//{
//    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "fa4c82d5b75e4cd351b1ea519c9dfd8312dea97c43f0aa13012d4ff2fd109763");
//});

//builder.Services.AddAuthentication(OAuthValidationDefaults.AuthenticationScheme)
//    .AddOAuthValidation();

//builder.Services.AddAuthentication()
//        .AddCookie(options =>
//        {
//            options.LoginPath = "/Account/Unauthorized/";
//            options.AccessDeniedPath = "/Account/Forbidden/";
//        })
//        .AddJwtBearer(options =>
//        {
//            options.Audience = "https://localhost:7270/";
//            options.Authority = "https://localhost:7270/";
//        });


//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(cfg =>
//    {
//        cfg.TokenValidationParameters = new TokenValidationParameters()
//        {
//            ValidateIssuer = true,
//            ValidIssuer = _config["Security:Tokens:Issuer"],
//            ValidateAudience = true,
//            ValidAudience = _config["Security:Tokens:Audience"],
//            ValidateIssuerSigningKey = true,
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Security:Tokens:Key"])),
//        };
//    });



//request.Headers.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: Token);//do sprawdzenia 

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication(); //TODO to dodane musi byc przed UseHttpsRedirection!!!

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
