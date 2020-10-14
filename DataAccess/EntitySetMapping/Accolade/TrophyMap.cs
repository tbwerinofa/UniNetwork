using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class TrophyMap : InsightEntityTypeConfiguration<Trophy>
    {
        public TrophyMap()
        {
            

        }

        public override void Configure(EntityTypeBuilder<Trophy> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex( a =>new {a.Name }).IsUnique();
                  modelBuilder.Property(a => a.Name).HasMaxLength(50);
            modelBuilder.Property(a => a.Name).HasMaxLength(250);
            modelBuilder.Property(a => a.Name).IsRequired();

            modelBuilder
                     .HasOne(u => u.Document)
                     .WithMany(u => u.Trophies)
                     .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedTrophies)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedTrophies)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
