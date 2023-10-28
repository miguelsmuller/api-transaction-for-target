using finance_api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Do Pacote DotNetEnv
Env.Load();

var connectionStringFromApp = builder.Configuration.GetConnectionString("DBEngineConnection");
var connectionStringFromEnv = Environment.GetEnvironmentVariable("DBEngineConnection");

builder.Configuration["ConnectionStrings:DBEngineConnection"] = connectionStringFromEnv ?? connectionStringFromApp;
var connectionString = builder.Configuration.GetConnectionString("DBEngineConnection");

// Do Pacote Entity
builder.Services.AddDbContext<TransactionContext>(
    opts => opts.UseSqlite(connectionString)
);

// Do pacote AutoMapper
builder.Services.AddAutoMapper(
    AppDomain.CurrentDomain.GetAssemblies()
);

builder.Services.AddControllers();

// Do Pacote NewtonSoft
builder.Services.AddControllers().AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FilmesAPI", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors(options =>
    {
//         options.WithOrigins("http://seufrontend.com")
//             .AllowAnyHeader()
//             .AllowAnyMethod();

        options.AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod();
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
