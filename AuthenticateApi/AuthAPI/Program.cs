using AuthAPI.Services.IServices;
using AuthAPI.Models;
using AuthAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using AuthAPI.Services.AuthServices;
using AuthAPI.Services.ConfirmationKeyGenerators;
using AuthAPI.Services.PasswordStrengthChecker;
using AuthAPI.Services.SendEmailService;
using AuthAPI.Services.TokenManager;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Swashbuckle.AspNetCore.Filters;

namespace AuthAPI
{
    public class Program
    {
        private static string CorsEnabled = "All cors enabled";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("AuthSettings:JwtOptions"));

            builder.Services.AddScoped<IAuth, AuthService>();
            builder.Services.AddScoped<IConfirmationKeyGenerate, ConfirmationKeyGenerator>();
            builder.Services.AddScoped<IPasswordManager, PasswordManager>();
            builder.Services.AddScoped<IEmailSenderService, FropsiEmailSender>();
            builder.Services.AddScoped<ITokenManager, TokenManager>();


            builder.Services.AddCors(options =>
            {
                options.AddPolicy(CorsEnabled,
                policy =>
                {
                    policy.WithOrigins("*")
                    .AllowAnyHeader()
                    .AllowAnyMethod().
                    AllowAnyOrigin();
                });
            });
            

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            builder.Services.AddAuthentication().AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration?.GetSection("AuthSettings:JwtOptions:Secret").Value!)),
                    ValidIssuer = builder.Configuration?.GetSection("AuthSettings:JwtOptions:Issuer").Value!,
                    ValidAudience = builder.Configuration?.GetSection("AuthSettings:JwtOptions:Audience").Value!,
                };
            });

            var app = builder.Build();

            if (!app.Environment.IsDevelopment() || app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors(CorsEnabled);

            app.MapControllers();

            app.Run();
        }
    }
}
