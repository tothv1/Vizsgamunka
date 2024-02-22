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
            builder.Services.AddScoped<IPasswordStrengthChecker, PasswordStrengthChecker>();
            builder.Services.AddScoped<IEmailSenderService, FropsiEmailSender>();
            builder.Services.AddScoped<ITokenManager, TokenManager>();


            builder.Services.AddCors(options =>
            {
                options.AddPolicy(CorsEnabled,
                policy =>
                {
                    policy.WithOrigins("*")
                    .AllowAnyHeader()
                    .AllowAnyMethod().AllowAnyOrigin();
                });
            });
            

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment() || app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
