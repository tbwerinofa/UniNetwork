using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class MessageTemplateMap : InsightEntityTypeConfiguration<MessageTemplate>
    {
        public MessageTemplateMap()
        {
            

        }

        public override void Configure(EntityTypeBuilder<MessageTemplate> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex( a =>new {a.EmailAccountId,a.Name }).IsUnique();

            modelBuilder.Property(a => a.EmailAccountId).IsRequired();
            modelBuilder.Property(a => a.Name).IsRequired();
            

            modelBuilder
                     .HasOne(u => u.EmailAccount)
                     .WithMany(u => u.MessageTemplates)
                     .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedMessageTemplates)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedMessageTemplates)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
