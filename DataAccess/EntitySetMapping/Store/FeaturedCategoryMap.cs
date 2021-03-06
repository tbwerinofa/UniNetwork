﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class FeaturedCategoryMap : InsightEntityTypeConfiguration<FeaturedCategory>
    {
        public FeaturedCategoryMap()
        {
        }

        public override void Configure(EntityTypeBuilder<FeaturedCategory> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex( a =>new { a.Name}).IsUnique();
            modelBuilder.Property(a => a.Name).IsRequired();

            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedFeaturedCategories)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedFeaturedCategories)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
