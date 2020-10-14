using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class QueuedEmailMap : InsightEntityTypeConfiguration<QueuedEmail>
    {
        public QueuedEmailMap()
        {
            

        }

        public override void Configure(EntityTypeBuilder<QueuedEmail> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");



            modelBuilder.Property(a => a.EmailAccountId).IsRequired();
            

            modelBuilder
                     .HasOne(u => u.EmailAccount)
                     .WithMany(u => u.QueuedEmails)
                     .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedQueuedEmails)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedQueuedEmails)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
