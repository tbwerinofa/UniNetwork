using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class AwardMap : InsightEntityTypeConfiguration<Award>
    {
        public AwardMap()
        {
            

        }

        public override void Configure(EntityTypeBuilder<Award> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex( a =>new { a.Name}).IsUnique();
            modelBuilder.Property(a => a.Name).IsRequired();
            modelBuilder.Property(a => a.FrequencyId).IsRequired();
            modelBuilder.Property(a => a.HasTrophy).IsRequired();

            modelBuilder
            .HasOne(u => u.Frequency)
            .WithMany(u => u.Awards)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
               .HasOne(u => u.Gender)
               .WithMany(u => u.Awards)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedAwards)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedAwards)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
