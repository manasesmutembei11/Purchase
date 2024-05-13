using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Purchase.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Purchase.Infrastructure.Configuration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasData
            (
            new OrderItem
            {
                Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                SubTotal = 300,
                OrderId = new Guid("c9d4c053-49b6-440c-bc78-2d54a9991870"),
                
            },
                  new OrderItem
                  {
                      Id = new Guid("c9d4c053-69b6-410c-bc78-2d54a9991870"),
                      SubTotal = 300,
                      OrderId = new Guid("c9d4c053-49b6-440c-bc78-2d54a9991870"),

                  }
            );
        }
    }
}
