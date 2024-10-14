using System;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class DishConfiguration : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.ToTable("Dishes");
            builder.HasKey(x => x.DishId);
            builder.Property(x => x.DishId).IsRequired().ValueGeneratedOnAdd();
            builder.HasOne<Category>(x => x.Category)
                .WithMany(x => x.Dishes)
                .HasForeignKey(x => x.CategoryID).IsRequired();
        }
    }
