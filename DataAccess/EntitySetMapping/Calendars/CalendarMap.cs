using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class CalendarMap : InsightEntityTypeConfiguration<Calendar>
    {
        public CalendarMap()
        {
            

        }

        public override void Configure(EntityTypeBuilder<Calendar> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");



            modelBuilder.HasIndex( a =>new { a.EventId, a.ScheduleDate, a.StartTime }).IsUnique();
            modelBuilder.Property(a => a.EventId).IsRequired();
            modelBuilder.Property(a => a.ScheduleDate).IsRequired();
            modelBuilder.Property(a => a.StartTime).IsRequired();
            modelBuilder.Property(a => a.FinYearId).IsRequired();

            modelBuilder
            .HasOne(u => u.Venue)
            .WithMany(u => u.Calendars)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                     .HasOne(u => u.Event)
                     .WithMany(u => u.Calendars)
                     .IsRequired()
                     .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.FinYear)
             .WithMany(u => u.Calendars)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedCalendars)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedCalendars)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
