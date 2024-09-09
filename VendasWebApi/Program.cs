using FluentValidation;
using FluentValidation.AspNetCore;
using MailKit;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using VendasWebApi;
using VendasWebApi.Filters;
using VendasWebApplication;
using VendasWebApplication.Commands.PedidoCommands.CriarPedido;
using VendasWebApplication.Validators;
using VendasWebCore.Repositories;
using VendasWebCore.Services;
using VendasWebInfrastructure.AuthService;
using VendasWebInfrastructure.Persistence;
using VendasWebInfrastructure.Persistence.Repositories;
using VendasWebInfrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddFluentValidationAutoValidation()
        .AddFluentValidationClientsideAdapters()
        .AddValidatorsFromAssemblyContaining<CriarPedidoCommandValidator>();

builder.Services.AddControllers(options => options.Filters.Add(typeof(ValidationFilter)));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddDbContext<VendasWebDbContext>
//    (options => options.UseSqlServer
//    (builder.Configuration.GetConnectionString("Default")));

builder.Services.AddDbContext<VendasWebDbContext>
    (options => options.UseInMemoryDatabase("tst"));

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddTransient<IEmailService, EmailSenderService>();

builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IItensPedidoRepository, ItensPedidoRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddApplication();
builder.Services.AddAuthenticationService(builder.Configuration);
builder.Services.AddSwaggerService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
