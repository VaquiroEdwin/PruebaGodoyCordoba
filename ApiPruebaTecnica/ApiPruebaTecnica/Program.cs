using ApiPruebaTecnica.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar la cadena de conexión a la base de datos  
builder.Services.AddDbContext<GodoyCordobaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BdGodoyCordoba")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
    builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
           .AllowAnyMethod()
           .AllowAnyHeader());



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
