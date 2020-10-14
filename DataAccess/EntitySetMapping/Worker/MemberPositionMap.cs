using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class MemberPositionMap : InsightEntityTypeConfiguration<MemberPosition>
    {
        public MemberPositionMap()
        {
            

        }

        public override void Configure(EntityTypeBuilder<MemberPosition> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            //modelBuilder.HasIndex( a =>new {a.EmployeeId,a.StartDate }).IsUnique();
            modelBuilder.Property(a => a.StartDate).IsRequired();
            modelBuilder.Property(a => a.MemberId).IsRequired();
            modelBuilder.Property(a => a.FinYearId).IsRequired();
            modelBuilder.Property(a => a.PositionAuditId).IsRequired();


      //      modelBuilder.HasOne(u => u.CreatedUser)
      //       .WithMany(u => u.CreatedMemberPositions)
      //       .IsRequired()
      //       .OnDelete(DeleteBehavior.Restrict);

      //      modelBuilder.HasOne(u => u.UpdatedUser)
      //     .WithMany(u => u.UpdatedMemberPositions)
      //     .OnDelete(DeleteBehavior.Restrict);

      //      modelBuilder.HasOne(u => u.Organogram)
      //        .WithMany(u => u.ShopStewards)
      //        .IsRequired()
      //        .OnDelete(DeleteBehavior.Cascade);

      //      modelBuilder.HasOne(u => u.PositionAudit)
      //     .WithMany(u => u.MemberPositions)
      //     .IsRequired()
      //     .OnDelete(DeleteBehavior.Restrict);

      //      modelBuilder.HasOne(u => u.Employee)
      //     .WithMany(u => u.MemberPositions)
      //     .OnDelete(DeleteBehavior.Cascade);

      //     modelBuilder.HasOne(u => u.Member)
      //    .WithMany(u => u.MemberPositions)
      //       .IsRequired()
      //    .OnDelete(DeleteBehavior.Cascade);

      //      modelBuilder.HasOne(u => u.FinYear)
      //.WithMany(u => u.MemberPositions)
      //.IsRequired()
      //.OnDelete(DeleteBehavior.Restrict);



        }
    }
}
