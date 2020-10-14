using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class FrequencyMap : InsightEntityTypeConfiguration<Frequency>
    {
        public FrequencyMap()
        {
            

        }

        public override void Configure(EntityTypeBuilder<Frequency> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex( a =>new { a.Name}).IsUnique();
            modelBuilder.Property(a => a.Name).HasMaxLength(50);

            modelBuilder.HasIndex(a => new { a.Discriminator }).IsUnique();
            modelBuilder.Property(a => a.Discriminator).HasMaxLength(4);

            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedFrequencies)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedFrequencies)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
