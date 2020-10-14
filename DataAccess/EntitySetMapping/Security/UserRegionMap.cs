using DataAccess.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DataMapping
{
   
    public partial class UserRegionMap : InsightEntityTypeConfiguration<UserRegion>
    {
        public UserRegionMap()
        {


        }

        public override void Configure(EntityTypeBuilder<UserRegion> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex(a => new { a.ApplicationUserId, a.RegionId }).IsUnique();
            modelBuilder.Property(a => a.ApplicationUserId).IsRequired();
            modelBuilder.Property(a => a.RegionId).IsRequired();

            modelBuilder
            .HasOne(u => u.Region)
            .WithMany(u => u.UserRegions)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
          .HasOne(u => u.ApplicationUser)
          .WithMany(u => u.UserRegions)
          .IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedUserRegions)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);


            modelBuilder
             .HasOne(u => u.UpdatedUser)
             .WithMany(u => u.UpdateUserRegions)
             .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
