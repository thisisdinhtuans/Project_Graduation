using System;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class OrderTableConfiguration : IEntityTypeConfiguration<OrderTable>
    {
        public void Configure(EntityTypeBuilder<OrderTable> builder)
        {
            builder.ToTable("OrderTables");
            builder.HasKey(x => new { x.OrderId, x.TableID });
            builder.HasOne(t => t.Table).WithMany(pc => pc.OrderTables)
                .HasForeignKey(pc => pc.TableID);

            builder.HasOne(t => t.Order).WithMany(pc => pc.OrderTables)
              .HasForeignKey(pc => pc.OrderId);
        }
    }
