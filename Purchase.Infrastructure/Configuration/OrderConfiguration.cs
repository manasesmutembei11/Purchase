using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Purchase.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Infrastructure.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasData
            (
            new Order
            {
                Id = new Guid("c9d4c053-49b6-440c-bc78-2d54a9991870"),
                Total = 300,
                CustomerId = new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6"),

            }
            );
        }
    }
}
