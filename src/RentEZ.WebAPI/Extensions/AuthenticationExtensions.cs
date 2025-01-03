using Microsoft.Extensions.Options;
using Utility.Config;

namespace RentEZ.WebAPI.Extensions;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Service.Utils;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddAuthenticationAndAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        // Add authentication
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = JwtUtils.GetTokenValidationParameters();
            })
            .AddGoogle(options =>
            {
                var googleSetting = GoogleSetting.Instance;
                options.ClientId = googleSetting.ClientID;
                options.ClientSecret = googleSetting.ClientSecret;
                options.Scope.Add("email");
                options.Scope.Add("profile");
                options.SaveTokens = true;
            });

        // Add authorization
        services.AddAuthorization(options =>
        {
            options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
            options.AddPolicy("RequireShopOwnerRole", policy => policy.RequireRole("ShopOwner"));
            options.AddPolicy("RequireCustomerRole", policy => policy.RequireRole("Customer"));
        });

        return services;
    }
}