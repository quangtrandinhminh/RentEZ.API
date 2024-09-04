using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Reflection;
using Serilog;
using BusinessObject.Entities.Identity;
using BusinessObject.Mapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Repository.Interfaces;
using Repository.Repositories;
using Service.Services;
using Service.Utils;
using Utility.Config;
using Service.Interfaces;
using RentEZ.WebAPI.Middlewares;
using Repository.Infrastructure;
using AngleSharp;
using AspNetCoreRateLimit;

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
/*builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptionsAction =>
        {
            sqlServerOptionsAction.MigrationsAssembly(
                typeof(AppDbContext).GetTypeInfo().Assembly.GetName().Name);
            sqlServerOptionsAction.MigrationsHistoryTable("Migration");
        }));*/

// Register DbContext Postgres
builder.Services.AddDbContext<AppDbContext>(options =>
       options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"),
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

var mailSettingModel = new MailSettingModel();
builder.Configuration.GetSection("MailSetting").Bind(mailSettingModel);
MailSettingModel.Instance = mailSettingModel;

var vnPaySetting = new VnPaySetting();
builder.Configuration.GetSection("VnPay").Bind(vnPaySetting);
VnPaySetting.Instance = vnPaySetting;

var vietQrSetting = new VietQRSetting();
builder.Configuration.GetSection("VietQR").Bind(vietQrSetting);
VietQRSetting.Instance = vietQrSetting;

var googleSetting = new GoogleSetting();
builder.Configuration.GetSection("Authentication:Google").Bind(googleSetting);
GoogleSetting.Instance = googleSetting;

// Add Identity Core
builder.Services.AddIdentityCore<UserEntity>()
    .AddRoles<RoleEntity>()
    .AddUserStore<UserRepository>()
    .AddEntityFrameworkStores<AppDbContext>();

// memory cache 
builder.Services.AddMemoryCache();

// rate limiter
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.Configure<IpRateLimitPolicies>(builder.Configuration.GetSection("IpRateLimitPolicies"));
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

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
    o.SwaggerDoc("v1", new OpenApiInfo { Title = "RentEZ", Version = "v1" });
});

// Add Authorization
//builder.Services.AddSingleton<IAuthorizationPolicyProvider, ApiPolicyAuthorizationProvider>();
//builder.Services.AddSingleton<IAuthorizationHandler, ApiPolicyAuthorizationHandler>();

// Add JWT
builder.Services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.UseSecurityTokenValidators = true;
    options.TokenValidationParameters = JwtUtils.GetTokenValidationParameters();
});

// Add Authentication
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
    })
    .AddGoogle(options =>
    {
        options.ClientId = googleSetting.ClientID;
        options.ClientSecret = googleSetting.ClientSecret;
        options.Scope.Add("email");
        options.Scope.Add("profile");
        options.SaveTokens = true;
    });


// Add Authorization
builder.Services.AddAuthorization(cfg =>
{
    cfg.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
    cfg.AddPolicy("RequireShopOwnerRole", policy => policy.RequireRole("ShopOwner"));
    cfg.AddPolicy("RequireCustomerRole", policy => policy.RequireRole("Customer"));
});

builder.Services.AddHttpContextAccessor();

//Add controllers
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();

// Add DI
builder.Services.AddScoped<MapperlyMapper>();
// Repository
builder.Services.AddScoped<AppDbContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Service
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmailService, EmailService>();

//-----------------------------------------------------------------------------------------------
var app = builder.Build();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseIpRateLimiting();

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
