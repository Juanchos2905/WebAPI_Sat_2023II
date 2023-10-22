using API_Sat_2023II.DAL;
using Microsoft.EntityFrameworkCore;
using WebAPI_Sat_2023II.Domain.Interfaces;
using WebAPI_Sat_2023II.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//dependecias de contenedor

builder.Services.AddControllers();

//Esta línea me crea el contexto de la BD a la hora de correr esta API
//Funciones Anónimas (x => x....) Arrow Functions - Lambda Functions
builder.Services.AddDbContext<DataBaseContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//aca interfaces por cada nuevo servico/interfaz
builder.Services.AddScoped<ICountryService, CountryService>();

/*Los tres ciclos de vida de los objetos
-Builder.Services.AddTransient
-Builder.Services.AddSingleton
-Builder.Services.AddScope

a nivel de .netcore

 */





//dependecias de swager por defecto
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//middleware con los pipelines
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
