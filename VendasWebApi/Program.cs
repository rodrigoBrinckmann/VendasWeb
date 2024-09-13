using FluentValidation;
using VendasWebApi.Extensions;
using VendasWebApplication;
using VendasWebApplication.Validators;
using VendasWebCore;
using VendasWebInfrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
        //.AddFluentValidationAutoValidation()
        //.AddFluentValidationClientsideAdapters()
        .AddValidatorsFromAssemblyContaining<CriarPedidoCommandValidator>();

builder.Services.AddControllers(); //(options => options.Filters.Add(new ValidationFilter()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddAuthenticationService(builder.Configuration);
builder.Services.AddWebCore();
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
