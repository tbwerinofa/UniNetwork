using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.DataMapping.Core
{
    public partial class IncidentTypeMap : InsightEntityTypeConfiguration<IncidentType>
    {
        public IncidentTypeMap()
        {
        }

        public override void Configure(EntityTypeBuilder<IncidentType> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
               modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.Property(a => a.Name).IsRequired();
            modelBuilder.Property(a => a.IncidentGroupId).IsRequired();

            modelBuilder.HasIndex(a => new { a.IncidentGroupId, a.Name }).IsUnique();

            modelBuilder
            .HasOne(u => u.CreatedUser)
            .WithMany(u => u.CreatedIncidentTypes)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
               .HasOne(u => u.UpdatedUser)
               .WithMany(u => u.UpdatedIncidentTypes)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .HasOne(u => u.IncidentGroup)
                .WithMany(u => u.IncidentTypes)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
