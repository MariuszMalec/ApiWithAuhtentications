using ApiWithAuhtentications.Authentication.ApiKey;
using ApiWithAuhtentications.Authentication.Authentication.ApiKey;
using ApiWithAuhtentications.Middleware;
using ApiWithAuhtentications.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddTransient<UserService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//must be added!
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;
builder.Services
    .AddAuthentication(sharedOptions =>
    {
        sharedOptions.DefaultScheme = ApiKeyAuthenticationOptions.AuthenticationScheme;
    })
    .AddApiKey<ApiKeyAuthenticationService>(options => configuration.Bind("ApiKeyAuth", options));


builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiWithAuhtenticationApiKey", Version = "v1" });
    option.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid apikey",
        Name = "ApiKey",
        Type = SecuritySchemeType.ApiKey,
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
                    Id="ApiKey"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<MyExceptionMiddleware>(); //lapanie wyjatkow

app.UseAuthentication(); //TODO to dodane

app.UseAuthorization();

app.MapControllers();

app.Run();
