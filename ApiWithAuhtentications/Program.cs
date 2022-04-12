using ApiWithAuhtentications.Authentication.ApiKey;
using ApiWithAuhtentications.Authentication.Authentication.ApiKey;
using ApiWithAuhtentications.Middleware;
using ApiWithAuhtentications.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddTransient<UserService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//must be added!
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;
builder.Services
    .AddAuthentication(sharedOptions =>
    {
        sharedOptions.DefaultScheme = ApiKeyAuthenticationOptions.AuthenticationScheme;
    })
    .AddApiKey<ApiKeyAuthenticationService>(options => configuration.Bind("ApiKeyAuth", options));

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
