using RentEZ.WebAPI.Extensions;
using Serilog;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables
Env.Load();
Console.WriteLine($"Environment: {Environment.GetEnvironmentVariable("APP_NAME")}");

// Configure logging
builder.Host.UseSerilog((ctx, config) => config.ReadFrom.Configuration(ctx.Configuration));

// Configure services
builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddAuthenticationAndAuthorization(builder.Configuration);
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

// Apply migrations
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

// Configure middleware
app.UseSwaggerDocumentation();
app.UseApplicationMiddleware();

app.MapControllers();
app.Run();