using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class DocumentMap : InsightEntityTypeConfiguration<Document>
    {
        public DocumentMap()
        {
            

        }

        public override void Configure(EntityTypeBuilder<Document> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex( a =>new {a.DocumentTypeId,a.DocumentNameGuid }).IsUnique();

            modelBuilder.Property(a => a.DocumentTypeId).IsRequired();
            modelBuilder.Property(a => a.DocumentNameGuid).IsRequired();
            

            modelBuilder
                     .HasOne(u => u.DocumentType)
                     .WithMany(u => u.Documents)
                     .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedDocuments)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedDocuments)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
