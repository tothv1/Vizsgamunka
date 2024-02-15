using AuthAPI.Services.IServices;
using AuthAPI.Models;
using AuthAPI.Services;
using AuthAPI.Services.TokenGenerators;
using Microsoft.EntityFrameworkCore;
using System;

namespace AuthAPI
{
    public class Program
    {
        private static string CorsEnabled = "All cors enabled";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("AuthSettings:JwtOptions"));

            builder.Services.AddDbContext<AuthContext>(option =>
            {
                var connectionString = builder.Configuration.GetConnectionString("MySql");
                option.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

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
            builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
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
