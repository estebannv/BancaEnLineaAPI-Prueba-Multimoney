using Ganss.Xss;
using MiBancaEnLineaAPI.Data;
using MiBancaEnLineaAPI.Funtions;
using MiBancaEnLineaAPI.Repositories.IRepositories;
using MiBancaEnLineaAPI.Repositories.Repositories;
using MiBancaEnLineaAPI.Services.IServices;
using MiBancaEnLineaAPI.Services.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("Connection");

builder.Services.AddDbContext<MiBancaEnLineaDbContext>(
    options => options.UseSqlServer(connectionString)
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(MiBancaEnLineaDbContext));
builder.Services.AddScoped(typeof(HtmlSanitizer));

//Repositories
builder.Services.AddScoped(typeof(ITransaccionRepository), typeof(TransaccionRepository));
builder.Services.AddScoped(typeof(ICuentaBancariaRepository), typeof(CuentaBancariaRepository));
builder.Services.AddSingleton(typeof(IInteresDiarioRepository), typeof(InteresDiarioRepository));

//Services
builder.Services.AddScoped(typeof(ITransaccionService), typeof(TransaccionService));
builder.Services.AddScoped(typeof(ICuentaBancariaService), typeof(CuentaBancariaService));
builder.Services.AddSingleton(typeof(IInteresDiarioService), typeof(InteresDiarioService));

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(150);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});

builder.Services.AddHostedService<TimedHostedService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseSession();

app.MapControllers();

app.Run();
