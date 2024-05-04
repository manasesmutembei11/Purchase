using Microsoft.EntityFrameworkCore;
using Purchase.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Purchase.Infrastructure
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
        : base(options)
        {
        }
        public DbSet<Customer>? Customers { get; set; }
        public DbSet<Order>? Orders { get; set; }

        public DbSet<OrderItem>? OrderItems {  get; set; }
        
        public DbSet<Product>? Products { get; set; }

        public DbSet<Category>? Categories { get; set; }

        public DbSet<Tax>? Taxes { get; set; }  
    }

}
