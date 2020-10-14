using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class SystemDocumentMap : InsightEntityTypeConfiguration<SystemDocument>
    {
        public SystemDocumentMap()
        {
            

        }

        public override void Configure(EntityTypeBuilder<SystemDocument> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex( a =>new {a.FinYearId,a.DocumentId,a.Ordinal }).IsUnique();

            modelBuilder.Property(a => a.FinYearId).IsRequired();
            modelBuilder.Property(a => a.DocumentId).IsRequired();
            modelBuilder.Property(a => a.Ordinal).IsRequired();

            modelBuilder
                     .HasOne(u => u.Document)
                     .WithMany(u => u.SystemDocuments)
                     .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedSystemDocuments)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedSystemDocuments)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
