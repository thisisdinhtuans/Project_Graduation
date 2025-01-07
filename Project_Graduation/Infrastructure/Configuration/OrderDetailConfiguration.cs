// using System;
// using Infrastructure.Entities;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;

// namespace Infrastructure.Configuration;

// public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
//     {
//         public void Configure(EntityTypeBuilder<OrderDetail> builder)
//         {
//             builder.ToTable("OrderDetails");
//             builder.HasKey(x => x.Id);
//             builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
//             builder.HasOne<Order>(x => x.Order)
//                 .WithMany(x => x.OrderDetails)
//                 .HasForeignKey(x => x.OrderId).IsRequired();
            
//                 // Quan hệ với bảng Dish
//             builder.HasOne<Dish>(x => x.Dish)
//                 .WithMany(x => x.OrderDetails)
//                 .HasForeignKey(x => x.DishId).IsRequired();
//         }
//     }
using System;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.ToTable("OrderDetails");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

        // Quan hệ với bảng Order
        builder.HasOne<Order>(x => x.Order)
            .WithMany(x => x.OrderDetails)
            .HasForeignKey(x => x.OrderId).IsRequired();

        // Quan hệ với bảng Dish
        builder.HasOne<Dish>(x => x.Dish)
            .WithMany(x => x.OrderDetails)
            .HasForeignKey(x => x.DishId).IsRequired();
    }
}