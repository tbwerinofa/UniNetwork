using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class CityMap : InsightEntityTypeConfiguration<City>
    {
        public CityMap()
        {
            

        }

        public override void Configure(EntityTypeBuilder<City> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex( a =>new {a.ProvinceId,a.Name }).IsUnique();

            modelBuilder.Property(a => a.ProvinceId).IsRequired();
            modelBuilder.Property(a => a.Name).IsRequired();
            

            modelBuilder
                     .HasOne(u => u.Province)
                     .WithMany(u => u.Cities)
                     .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedCities)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedCities)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
