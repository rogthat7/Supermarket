using Supermarket.API.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Supermarket.API.Domain.Models;

namespace Supermarket.API.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<ApplicationUser>().ToTable("AspNetUsers");
            builder.Entity<ApplicationUser>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<ApplicationUser>().Property(p => p.UserId).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<ApplicationUser>().Property(p => p.FirstName).IsRequired().HasMaxLength(50);
            builder.Entity<ApplicationUser>().Property(p => p.LastName).IsRequired().HasMaxLength(50);
            builder.Entity<ApplicationUser>().Property(p => p.PhoneNumber).HasMaxLength(10);
            builder.Entity<ApplicationUser>().Property(p => p.UserName).HasMaxLength(30);
            builder.Entity<ApplicationUser>().Property(p => p.AadharNumber).HasMaxLength(12);
            builder.Entity<ApplicationUser>().Property(p => p.Email).HasMaxLength(30);
            builder.Entity<ApplicationUser>().Property(p => p.Address).HasMaxLength(100);
            builder.Entity<ApplicationUser>().Property(p => p.PasswordHash).HasMaxLength(256);

        }
         DbSet<ApplicationUser> applicationUsers {get; set;}
    }
}
