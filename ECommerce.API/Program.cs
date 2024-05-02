using ECommerce.API.ECommerce.Application.Interfaces;
using ECommerce.API.ECommerce.Application.Repositories;
using ECommerce.API.ECommerce.Domain;
using ECommerce.API.ECommerce.Domain.Model;
using ECommerce.API.ECommerce.Infrastructure;
using ECommerce.API.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace ECommerce.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            // Add services to the container.
            builder.Services.AddSingleton(configuration);
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ICategory, CategoryRepo>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            //builder.Services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            builder.Services.Configure<MailSetting>(configuration.GetSection("MailSettings"));
            builder.Services.AddTransient<IMailRepo, MailRepo>();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["Jwt:ValidIss"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Jwt:ValidAud"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("HelloFromECommerceWebsite5HelloFromECommerceWebsite5"))
                };
            });

            // inject for database
            builder.Services.AddDbContext<ECommerceDbContext>(optionBuilder =>
            {
                optionBuilder.UseSqlServer(configuration.GetConnectionString("cs"));
            });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ECommerceDbContext>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            // Configure CORS
            app.UseCors(builder =>
            {
                builder.WithOrigins(configuration["Jwt:ValidAud"])
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}