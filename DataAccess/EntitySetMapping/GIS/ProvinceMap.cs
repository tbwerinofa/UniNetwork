using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class ProvinceMap : InsightEntityTypeConfiguration<Province>
    {
        public ProvinceMap()
        {
            

        }

        public override void Configure(EntityTypeBuilder<Province> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex( a =>new {a.CountryId,a.Name }).IsUnique();

            modelBuilder.Property(a => a.CountryId).IsRequired();
            modelBuilder.Property(a => a.Name).IsRequired();
            

            modelBuilder
                     .HasOne(u => u.Country)
                     .WithMany(u => u.Provinces)
                     .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedProvinces)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedProvinces)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
