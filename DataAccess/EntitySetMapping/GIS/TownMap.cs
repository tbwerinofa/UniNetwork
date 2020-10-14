using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class TownMap : InsightEntityTypeConfiguration<Town>
    {
        public TownMap()
        {
            

        }

        public override void Configure(EntityTypeBuilder<Town> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex( a =>new {a.CityId,a.Name }).IsUnique();

            modelBuilder.Property(a => a.CityId).IsRequired();
            modelBuilder.Property(a => a.Name).IsRequired();
            

            modelBuilder
                     .HasOne(u => u.City)
                     .WithMany(u => u.Towns)
                     .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedTowns)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedTowns)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
