using ApiWithAuhtenticationBearer.Services;
using AspNet.Security.OAuth.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http.Headers;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddTransient<UserService>();

//nie dziala!
builder.Services.AddHttpClient("ApiWithBearer", httpClient =>
{
    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "fa4c82d5b75e4cd351b1ea519c9dfd8312dea97c43f0aa13012d4ff2fd109763");
});

builder.Services.AddAuthentication(OAuthValidationDefaults.AuthenticationScheme)
    .AddOAuthValidation();

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

app.UseHttpsRedirection();

app.UseAuthentication(); //TODO to dodane

app.UseAuthorization();

app.MapControllers();

app.Run();
