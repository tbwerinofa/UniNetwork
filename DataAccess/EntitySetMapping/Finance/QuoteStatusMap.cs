﻿using DataAccess.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DataMapping
{
    public partial class QuoteStatusMap : InsightEntityTypeConfiguration<QuoteStatus>
    {
        public QuoteStatusMap()
        {
        }

        public override void Configure(EntityTypeBuilder<QuoteStatus> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex(a => new { a.Name }).IsUnique();
            modelBuilder.Property(a => a.Name).HasMaxLength(50);

            modelBuilder.HasIndex(a => new { a.Discriminator }).IsUnique();
            modelBuilder.Property(a => a.Discriminator).HasMaxLength(4);

            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedQuoteStatuses)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedQuoteStatuses)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
