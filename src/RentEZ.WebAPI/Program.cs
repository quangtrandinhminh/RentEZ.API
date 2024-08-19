using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Reflection;
using Serilog;
using BusinessObject.Entities.Identity;
using BusinessObject.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Repository.Base;
using Repository.Interfaces;
using Repository.Repositories;
using Service.Services;
using Service.Utils;
using System;
using Repository;
using Utility.Config;
using Service.Interfaces;
using RentEZ.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
/*builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));*/

// Add CORS policy
const string myAllowSpecificOrigins = "http://localhost:5173";
builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly(), true);
builder.Services.AddCors(options =>
{
    options.AddPolicy(myAllowSpecificOrigins,
    policy =>
    {
            policy
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
            //.AllowCredentials();
        });
});

// Add serilog and get configuration from appsettings.json
builder.Services.AddSerilog(config => { config.ReadFrom.Configuration(builder.Configuration); });

// Register DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptionsAction =>
        {
            sqlServerOptionsAction.MigrationsAssembly(
                typeof(AppDbContext).GetTypeInfo().Assembly.GetName().Name);
            sqlServerOptionsAction.MigrationsHistoryTable("Migration");
        }));

// Add system setting from appsettings.json
var systemSettingModel = new SystemSettingModel();
builder.Configuration.GetSection("SystemSetting").Bind(systemSettingModel);
SystemSettingModel.Instance = systemSettingModel;

var vnPaySetting = new VnPaySetting();
builder.Configuration.GetSection("VnPaySetting").Bind(vnPaySetting);
VnPaySetting.Instance = vnPaySetting;

// Add Identity
builder.Services.AddIdentity<UserEntity, RoleEntity>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddSwaggerGen(o =>
{
    o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "AddAuthorization",
        Description = "Give me bearer token to call API here!",
        Type = SecuritySchemeType.Http
    });
    o.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
    o.SwaggerDoc("v1", new OpenApiInfo { Title = "PetHealthCareSystem", Version = "v1" });
});

// Add Authorization
//builder.Services.AddSingleton<IAuthorizationPolicyProvider, ApiPolicyAuthorizationProvider>();
//builder.Services.AddSingleton<IAuthorizationHandler, ApiPolicyAuthorizationHandler>();
builder.Services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.UseSecurityTokenValidators = true;
    options.TokenValidationParameters = JwtUtils.GetTokenValidationParameters();
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.UseSecurityTokenValidators = true;
        options.TokenValidationParameters = JwtUtils.GetTokenValidationParameters();
    });

// Add Authorization
builder.Services.AddAuthorization(cfg =>
{
    cfg.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
    cfg.AddPolicy("RequireCustomerRole", policy => policy.RequireRole("Customer"));
});

builder.Services.AddHttpContextAccessor();

//Add controllers
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();

// Add DI
builder.Services.AddScoped<MapperlyMapper>();
// Repository
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Service
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();

//-----------------------------------------------------------------------------------------------
var app = builder.Build();
app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseCors(myAllowSpecificOrigins);

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
