using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Data;
using WebAPI;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<WebAPIContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("WebAPIContext") ?? throw new InvalidOperationException("Connection string 'WebAPIContext' not found.")));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPlayersEndpoints();



app.Run();

