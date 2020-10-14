using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class EventMap : InsightEntityTypeConfiguration<Event>
    {
        public EventMap()
        {
            

        }

        public override void Configure(EntityTypeBuilder<Event> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");



            modelBuilder.HasIndex( a =>new {a.EventTypeId, a.Name }).IsUnique();
                  modelBuilder.Property(a => a.Name).HasMaxLength(50);
            modelBuilder.Property(a => a.EventTypeId).IsRequired();
            modelBuilder.Property(a => a.FrequencyId).IsRequired();
            modelBuilder.Property(a => a.RequiresRsvp).IsRequired();
            modelBuilder.Property(a => a.RequiresSubscription).IsRequired();

            modelBuilder
                     .HasOne(u => u.Frequency)
                     .WithMany(u => u.Events)
                     .IsRequired()
                     .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.EventType)
             .WithMany(u => u.Events)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedEvents)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedEvents)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
