using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Purchase.Domain.Models;
using Purchase.Domain.Models.Counters;
using Purchase.Domain.Models.UserEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Purchase.Infrastructure
{
    public class RepositoryContext : IdentityDbContext<User, Role, Guid>
    {
        private readonly ILogger<RepositoryContext> _logger;

        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        {
            
            _logger.LogDebug("Create AppDbContext");
        }
        public DbSet<Customer>? Customers { get; set; }
        public DbSet<AppCounter> Counters { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<OrderItem>? OrderItems { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Tax>? Taxes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(b =>
            {
                b.ToTable("Users");
            });

            builder.Entity<IdentityUserClaim<Guid>>(b =>
            {
                b.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<Guid>>(b =>
            {
                b.ToTable("UserLogins");
            });

            builder.Entity<IdentityUserToken<Guid>>(b =>
            {
                b.ToTable("UserTokens");
            });

            builder.Entity<Role>(b =>
            {
                b.ToTable("xpa_Roles");
            });

            builder.Entity<IdentityRoleClaim<Guid>>(b =>
            {
                b.ToTable("RoleClaims");
            });

            builder.Entity<IdentityUserRole<Guid>>(b =>
            {
                b.ToTable("UserRoles");
            });



            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            
        }
        public string GetTableName<TEntity>() where TEntity : class
        {
            var entityType = this.Model.FindEntityType(typeof(TEntity));
            var tableName = entityType.GetTableName();
            return tableName;
        }

    }
}
