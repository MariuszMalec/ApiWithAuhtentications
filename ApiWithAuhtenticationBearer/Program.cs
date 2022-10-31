using ApiWithAuhtenticationBearer.Entities;
using ApiWithAuhtenticationBearer.Interfaces;
using ApiWithAuhtenticationBearer.Middleware;
using ApiWithAuhtenticationBearer.Models;
using ApiWithAuhtenticationBearer.PolicyAuthorization;
using ApiWithAuhtenticationBearer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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

//dodanie dostepu w controlerze na poziomie policy
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("HasNationality", builder => builder.RequireClaim("Nationality", "polish"));//wymagany jest nationality claim przy endpoincie
    options.AddPolicy("Atleast20", builder => builder.AddRequirements(new MinimumAgeRequirement(25)));//wlasna polityka
});
//https://youtu.be/Ei7Uk-UgSAY?t=1355

builder.Services.AddControllers();

builder.Services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();//TODO nie dziala!!??
builder.Services.AddTransient<UserService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IAccountService, AccountService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ErrorHandlingMiddleware>();

//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Authorize API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
            {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
                new string[]{}
            }
                });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    //how to add here seedera
    //var seeder = scope.ServiceProvider.GetRequiredService<UserSeeder>();
    //seeder.Seed();
    //SeedData.SeedRole();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication(); //TODO to dodane musi byc przed UseHttpsRedirection!!!

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
