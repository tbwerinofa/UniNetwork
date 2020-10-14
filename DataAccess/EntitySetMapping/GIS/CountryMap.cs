using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class CountryMap : InsightEntityTypeConfiguration<Country>
    {
        public CountryMap()
        {
            

        }

        public override void Configure(EntityTypeBuilder<Country> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex( a =>new {a.GlobalRegionId,a.Name }).IsUnique();

            modelBuilder.Property(a => a.GlobalRegionId).IsRequired();
            modelBuilder.Property(a => a.Name).IsRequired();
            

            modelBuilder
                     .HasOne(u => u.GlobalRegion)
                     .WithMany(u => u.Countries)
                     .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedCountries)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedCountries)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
