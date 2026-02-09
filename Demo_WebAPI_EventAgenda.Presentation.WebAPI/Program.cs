using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Repositories;
using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Services;
using Demo_WebAPI_EventAgenda.ApplicationCore.Services;
using Demo_WebAPI_EventAgenda.Infrastructure.Database;
using Demo_WebAPI_EventAgenda.Infrastructure.Database.Repositories;
using Demo_WebAPI_EventAgenda.Presentation.WebAPI.ExceptionHandlers;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container. (-> Injection of dependencies)
// Configuration of the following methods:
// AddSingleton : The goal is to create a single instance of a service and save in the memory.
// AddScoped : The goal is to create an instance per request
// AddTransient : The goal is to create a new instance every time it is requested.


// DI Configuration
// - Services (TODO)
builder.Services.AddScoped<IAgendaEventService, AgendaEventService>();
builder.Services.AddScoped<IFaqService, FaqService>();
// - Repositories
builder.Services.AddScoped<IAgendaEventRepository, AgendaEventRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IFaqRepository, FaqRepository>();
//...
// - DbContext (TODO)
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));

    //The method "GetConnectionString" permet d'objet la connection suivante
    // - "Data Source=(localdb)\\MSSQLLocalDB; : db server instance
    // - Initial Catalog=digital_agenda_db;    : base name
    // - Integrated Security=True;             : Connection login with Windows credentials - Development only
    // - Trust Server Certificate=True;"       : Valid SSL certificate - Development only
});

//Mapping of the controllers (Presentation layer)
builder.Services.AddControllers();
// Manage exceptions globally (Pattern "ExceptionHandler")
builder.Services.AddExceptionHandler<AgendaEventExceptionHandler>();
builder.Services.AddProblemDetails();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseAuthorization();

app.MapControllers();

app.Run();
