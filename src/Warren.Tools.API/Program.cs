using Microsoft.EntityFrameworkCore;
using Warren.Tools.Application.Mapper;
using Warren.Tools.Application.Services;
using Warren.Tools.Domain.Interfaces.Repositories;
using Warren.Tools.Domain.Interfaces.Services;
using Warren.Tools.Infra.Context;
using Warren.Tools.Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(5, 7)) // Versão 5.7 do MySQL
    ));
// Add services to the container.

// Registrar serviços
builder.Services.AddScoped<IBoletaService, BoletaService>(); 
builder.Services.AddScoped<IBoletaRepository, BoletaRepository>(); 
builder.Services.AddScoped<IPuRepository, PuRepository>(); 


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfile)); // Registra o MappingProfile


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
