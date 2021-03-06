﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.DataMapping.Core
{
    public partial class IncidentGroupMap : InsightEntityTypeConfiguration<IncidentGroup>
    {
        public IncidentGroupMap()
        {
        }

        public override void Configure(EntityTypeBuilder<IncidentGroup> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
               modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.Property(a => a.Name).IsRequired();
            modelBuilder.Property(a => a.Name).HasMaxLength(50);

            modelBuilder
            .HasOne(u => u.CreatedUser)
            .WithMany(u => u.CreatedIncidentGroups)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
               .HasOne(u => u.UpdatedUser)
               .WithMany(u => u.UpdatedIncidentGroups)
               .OnDelete(DeleteBehavior.Restrict);

        }

    }
}
