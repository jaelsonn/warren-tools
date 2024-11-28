using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Warren.Tools.Application.Mapper;
using Warren.Tools.Application.Services;
using Warren.Tools.Domain.Interfaces.Repositories;
using Warren.Tools.Domain.Interfaces.Services;
using Warren.Tools.Domain.Services;
using Warren.Tools.Infra.Context;
using Warren.Tools.Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(5, 7)) // Vers�o 5.7 do MySQL
    ));
// Add services to the container.

// Registrar servi�os
builder.Services.AddScoped<IBoletaService, BoletaService>(); 
builder.Services.AddScoped<IBoletaRepository, BoletaRepository>(); 
builder.Services.AddScoped<IPuRepository, PuRepository>();
builder.Services.AddScoped<IRoboService, RoboService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfile)); // Registra o MappingProfile

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals;
    });


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseSwaggerUI(c =>
    //{
    //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DemoAPI v1");
    //});
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
