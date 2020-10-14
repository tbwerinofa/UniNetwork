using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class MemberLicenseMap : InsightEntityTypeConfiguration<MemberLicense>
    {
        public MemberLicenseMap()
        {
            

        }

        public override void Configure(EntityTypeBuilder<MemberLicense> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex( a =>new {a.FinYearId,a.MemberId }).IsUnique();
            modelBuilder.Property(a => a.FinYearId).IsRequired();
            modelBuilder.Property(a => a.MemberId).IsRequired();


            modelBuilder.HasOne(u => u.FinYear)
        .WithMany(u => u.MemberLicenses)
        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.Member)
            .WithMany(u => u.MemberLicenses)
            .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedMemberLicenses)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedMemberLicenses)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
