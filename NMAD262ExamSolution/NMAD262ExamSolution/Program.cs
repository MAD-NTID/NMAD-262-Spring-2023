using Microsoft.EntityFrameworkCore;
using NMAD262ExamSolution.Databases;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//connecting to the database
string connection = builder.Configuration.GetConnectionString("mysqlDatabaseStringmysqlDatabaseString");
builder.Services.AddDbContextPool<MySqlDatabase>(options =>
    options.UseMySql(connection,ServerVersion.AutoDetect(connection))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();