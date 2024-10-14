using System;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class AreaConfiguration : IEntityTypeConfiguration<Area>
    {
        public void Configure(EntityTypeBuilder<Area> builder)
        {
            builder.ToTable("Areas");

            builder.HasKey(x => x.AreaID);
            builder.Property(x => x.AreaID).IsRequired().ValueGeneratedOnAdd();
            builder.HasOne<Restaurant>(x => x.Restaurant)
                .WithMany(x => x.Areas)
                .HasForeignKey(x => x.RestaurantID).IsRequired();
        }
    }
