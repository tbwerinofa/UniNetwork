using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.DataMapping.Core
{
    public partial class VenueMap : InsightEntityTypeConfiguration<Venue>
    {
        public VenueMap()
        {
        }

        public override void Configure(EntityTypeBuilder<Venue> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
               modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.Property(a => a.Name).IsRequired();
            modelBuilder.HasIndex(a => new { a.Name }).IsUnique();

            modelBuilder
            .HasOne(u => u.CreatedUser)
            .WithMany(u => u.CreatedVenues)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
               .HasOne(u => u.UpdatedUser)
               .WithMany(u => u.UpdatedVenues)
               .OnDelete(DeleteBehavior.Restrict);

        }

    }
}
