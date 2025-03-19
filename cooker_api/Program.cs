using Microsoft.EntityFrameworkCore;
using cooker_api.entities;
using Serilog;
using FluentValidation;
using cooker_api.Services;
using cooker_api.Validators;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((context, services, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration)
                 .Enrich.FromLogContext()
                 .WriteTo.Console()
                 .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

//validators
builder.Services.AddValidatorsFromAssemblyContaining<PostModelValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<FavouriteModelValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CommentModelValidator>();

//mediatR
builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(Program).Assembly));

//CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

//configure sql server
builder.Services.AddEntityFrameworkSqlServer();
builder.Services.AddDbContextPool<CookerContext>(options =>
{
    var conString = configuration.GetConnectionString("SqlServerDB");
    options.UseSqlServer(conString);
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<PostService>();
builder.Services.AddTransient<FavouriteService>();
builder.Services.AddTransient<UserService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
