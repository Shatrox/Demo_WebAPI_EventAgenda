using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Repositories;
using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Services;
using Demo_WebAPI_EventAgenda.ApplicationCore.Services;
using Demo_WebAPI_EventAgenda.Infrastructure.Database;
using Demo_WebAPI_EventAgenda.Infrastructure.Database.Repositories;
using Demo_WebAPI_EventAgenda.Presentation.WebAPI.Configs;
using Demo_WebAPI_EventAgenda.Presentation.WebAPI.ExceptionHandlers;
using Demo_WebAPI_EventAgenda.Presentation.WebAPI.Token;
using Demo_WebAPI_EventAgenda_Mail;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Scalar.AspNetCore;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container. (-> Injection of dependencies)
// Configuration of the following methods:
// AddSingleton : The goal is to create a single instance of a service and save in the memory.
// AddScoped : The goal is to create an instance per request
// AddTransient : The goal is to create a new instance every time it is requested.


// DI Configuration
builder.Services.AddSingleton<TokenTool>();
builder.Services.AddSingleton<IEmailService, MailKitConfig>();
// - Services (TODO)
builder.Services.AddScoped<IAgendaEventService, AgendaEventService>();
builder.Services.AddScoped<IMemberService, MemberService>();
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
builder.Services.AddOpenApi(options => {

    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info = new()
        {
            Title = "Agenda Event API",
            Version = "v1",
            Description = "Démo d'une API RESTFull pour le groupe .Net React de DigitalCity"
        };
        return Task.CompletedTask;
    });
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();

});

// Configuration of the authentication with JWT (JSON Web Token)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    byte[] secretKey = Encoding.UTF8.GetBytes(builder.Configuration["Token:Key"]!);

                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        // The method below allows to validate the signature of the token with the same key stored in the appsettings.json file (Token:Key)
                        ValidIssuer = builder.Configuration["Token:Issuer"],
                        ValidAudience = builder.Configuration["Token:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(secretKey),

                        // Rules to validate the signature of the token
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey =true,
                        ValidateLifetime = true,
                    };


                });

///----------------------------------------------------------------------------------------------------------------------

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseAuthentication(); // The method "UseAuthentication" allows to use the authentication middleware, which will validate the token in the request header and set the user identity in the HttpContext.User property 

app.UseAuthorization();

app.MapControllers();

app.Run();


