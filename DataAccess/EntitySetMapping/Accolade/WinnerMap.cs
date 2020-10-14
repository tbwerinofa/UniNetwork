using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class WinnerMap : InsightEntityTypeConfiguration<Winner>
    {
        public WinnerMap()
        {
        }

        public override void Configure(EntityTypeBuilder<Winner> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");



            modelBuilder.HasIndex( a =>new {a.FinYearId, a.CalendarMonthId,a.MemberId }).IsUnique();
                  modelBuilder.Property(a => a.FinYearId).HasMaxLength(50);
           // modelBuilder.Property(a => a.CalendarMonthId).IsRequired();
            modelBuilder.Property(a => a.MemberId).IsRequired();

            modelBuilder
                     .HasOne(u => u.FinYear)
                     .WithMany(u => u.Winners)
                     .IsRequired()
                     .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                  .HasOne(u => u.Award)
                  .WithMany(u => u.Winners)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.CalendarMonth)
             .WithMany(u => u.Winners)
           //  .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
           .HasOne(u => u.Member)
           .WithMany(u => u.Winners)
           .IsRequired()
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
           .HasOne(u => u.Award)
           .WithMany(u => u.Winners)
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedWinners)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedWinners)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
