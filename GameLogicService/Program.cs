using Microsoft.EntityFrameworkCore;
using GameLogicService.DataContext;
using GameLogicService.Services.Interfaces;
using GameLogicService.Services;
using GameLogicService.Repositories.Entity.Interfaces;
using GameLogicService.Repositories.Entity;
using GameLogicService.RestClientRequests.Interfaces;
using GameLogicService.RestClientRequests;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using GameLogicService.Messaging.Interfaces;
using GameLogicService.Messaging;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddScoped(sp =>
{
    var client = new HttpClient
    {
    };
    return client;
});

// Add services to the container.
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = "https://dev-he67eqpc846lev05.us.auth0.com/";
    options.Audience = "https://gamelogicservice.azurewebsites.net/api"; 
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        NameClaimType = ClaimTypes.NameIdentifier
    };
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//Register Client Services
builder.Services.AddScoped<IAuthAPIRequests, AuthAPIRequests>();
builder.Services.AddScoped<ITriviaAPIRequests, TriviaAPIRequests>();
builder.Services.AddScoped<IMessageSender, MessageSender>();

//Register Services
builder.Services.AddScoped<IServiceProduct, ServiceProduct>();
builder.Services.AddScoped<IGameAccountService, GameAccountService>();
builder.Services.AddScoped<IGameOptionsService, GameOptionsService>();
builder.Services.AddScoped<IPlayedQuizService, PlayedQuizService>();

//Register Repositories
builder.Services.AddScoped<IGameAccountRepository, GameAccountRepository>();
builder.Services.AddScoped<IGameOptionsRepository, GameOptionsRepository>();
builder.Services.AddScoped<IGameCategoryRepository, GameCategoryRepository>();
builder.Services.AddScoped<IGameCategoryTagRepository, GameCategoryTagRepository>();
builder.Services.AddScoped<IPlayedQuizRepository, PlayedQuizRepository>();
builder.Services.AddScoped<IPlayedQuizGameAccountRepository, PlayedQuizGameAccountRepository>();
builder.Services.AddScoped<IGameAccountAuthRepository, GameAccountAuthRepository>();

var app = builder.Build();
app.UseCors(builder =>
{
    builder.AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed((host) => true);
});


using (var scope = app.Services.CreateScope())
{
    using (var db = scope.ServiceProvider.GetService<DatabaseContext>())
    {
        if (!db?.Database?.CanConnect() == true)
        {
            throw new ArgumentException($"Failed to connect to DB using connectionstring {connectionString}");
        }
    }
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");
});

app.Run();



