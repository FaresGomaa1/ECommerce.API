using ECommerce.API.ECommerce.Domain.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.ECommerce.Infrastructure
{
    public class ECommerceDbContext : IdentityDbContext<ApplicationUser>
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().HasIndex(u => u.Email).IsUnique();
        }
    }
}