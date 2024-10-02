using FluentValidation;
using VendasWebApi.Extensions;
using VendasWebApplication;
using VendasWebApplication.Validators;
using VendasWebCore;
using VendasWebInfrastructure;
using VendasWebInfrastructure.AuthServices;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json",
        optional: true,
        reloadOnChange: true);

// Add services to the container.
builder.Services
        //.AddFluentValidationAutoValidation()
        //.AddFluentValidationClientsideAdapters()
        .AddValidatorsFromAssemblyContaining<CriarPedidoCommandValidator>();

builder.Services.AddControllers(); //(options => options.Filters.Add(new ValidationFilter()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddInfrastructure();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddEmailService(builder.Configuration);
builder.Services.AddRabbitMq(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddAuthenticationService(builder.Configuration);
builder.Services.AddWebCore();
builder.Services.AddSwaggerService();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IAuthenticatedUser, AuthenticatedUser>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
