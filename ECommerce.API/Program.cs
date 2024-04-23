using ECommerce.API.ECommerce.Application.Interfaces;
using ECommerce.API.ECommerce.Domain;
using ECommerce.API.ECommerce.Domain.Model;
using ECommerce.API.ECommerce.Infrastructure;
using ECommerce.API.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            //builder.Services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}