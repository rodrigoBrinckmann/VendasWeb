using Microsoft.EntityFrameworkCore;
using VendasWebCore.Repositories;
using VendasWebCore.Services;
using VendasWebApplication.Services;
using VendasWebInfrastructure.Persistence;
using VendasWebInfrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<VendasWebDbContext>
    (options => options.UseSqlServer
    (builder.Configuration.GetConnectionString("Default")));
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IItensPedidoRepository, ItensPedidoRepository>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IItensPedidoService, ItensPedidoService>();
builder.Services.AddSwaggerGen();

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
