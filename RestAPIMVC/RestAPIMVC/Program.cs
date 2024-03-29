using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NLog;
using RestAPIMVC.Databases;
using RestAPIMVC.Exceptions;
using RestAPIMVC.Models;
using RestAPIMVC.Repositories;
using RestAPIMVC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//add nlog config
string nlogPath = Directory.GetCurrentDirectory() + "/nlog.config";
LogManager.LoadConfiguration(nlogPath);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


string connection = builder.Configuration.GetConnectionString("mysqldbcredential");
builder.Services.AddDbContextPool<MySqlDatabase>(options =>
    options.UseMySql(connection,ServerVersion.AutoDetect(connection))
);

builder.Services
    .AddAuthentication("APIAuthentication")
    .AddScheme<AuthenticationSchemeOptions, APIAuthenticationAttribute>("APIAuthentication",null);

string paypal = builder.Configuration.Get("paypal");
dynamic cred = JsonConvert.DeserializeObject<dynamic>(paypal);
PaypalCredential credential = new PaypalCredential() { ClientId = cred.ClientId, ClientSecret = cred.ClientSecret };


builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<ICastRepository, CastRepository>();
builder.Services.AddScoped<IActorRepository, ActorRepository>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();