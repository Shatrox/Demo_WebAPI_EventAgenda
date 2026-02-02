using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces;
using Demo_WebAPI_EventAgenda.Infrastructure.Database;
using Demo_WebAPI_EventAgenda.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. (-> Injection of dependencies)
// Configuration of the following methods:
// AddSingleton : The goal is to create a single instance of a service and save in the memory.
// AddScoped : The goal is to create an instance per request
// AddTransient : The goal is to create a new instance every time it is requested.


// DI Configuration
// - Services (TODO)
// - Repositories
builder.Services.AddScoped<IAgendaEventRepository, AgendaEventRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
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


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
