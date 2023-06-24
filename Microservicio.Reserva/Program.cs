using Application.Interfaces;
using Application.UseCase.Facturas;
using Application.UseCase.MetodosPagos;
using Application.UseCase.Pagos;
using Application.UseCase.Reservas;
using Application.UserServices;
using Infrastructure.Command;
using Infrastructure.Persistence;
using Infrastructure.Querys;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Custom
var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<ReservaContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IFacturaService, FacturaService>();
builder.Services.AddScoped<IFacturaCommand, FacturaCommand>();
builder.Services.AddScoped<IFacturaQuery, FacturaQuery>();

builder.Services.AddScoped<IMetodoPagoService, MetodoPagoService>();
builder.Services.AddScoped<IMetodoPagoCommand, MetodoPagoCommand>();
builder.Services.AddScoped<IMetodoPagoQuery, MetodoPagoQuery>();

builder.Services.AddScoped<IPagoService, PagoService>();
builder.Services.AddScoped<IPagoCommand, PagoCommand>();
builder.Services.AddScoped<IPagoQuery, PagoQuery>();

builder.Services.AddScoped<IReservaService, ReservaService>();
builder.Services.AddScoped<IReservaCommand, ReservaCommand>();
builder.Services.AddScoped<IReservaQuery, ReservaQuery>();

builder.Services.AddScoped<IUserServiceViaje, UserServiceViaje>();

//CORS deshabilitar
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
