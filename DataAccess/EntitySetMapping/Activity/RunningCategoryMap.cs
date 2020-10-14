﻿using DataAccess.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DataMapping
{
   
    public partial class RunningCategoryMap : InsightEntityTypeConfiguration<RunningCategory>
    {
        public RunningCategoryMap()
        {


        }

        public override void Configure(EntityTypeBuilder<RunningCategory> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex(a => new { a.Name }).IsUnique();
            modelBuilder.Property(a => a.Name).IsRequired();

            modelBuilder
             .HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedRunningCategories)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.UpdatedUser)
             .WithMany(u => u.UpdatedRunningCategories)
             .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
