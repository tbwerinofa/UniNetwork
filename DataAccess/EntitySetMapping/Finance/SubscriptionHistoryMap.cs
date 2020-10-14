using DataAccess.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DataMapping
{
    public partial class SubscriptionHistoryMap : InsightEntityTypeConfiguration<SubscriptionHistory>
    {
        public SubscriptionHistoryMap()
        {
        }

        public override void Configure(EntityTypeBuilder<SubscriptionHistory> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex( a =>new { a.QuoteDetailId }).IsUnique();
            modelBuilder.Property(a => a.StartDate).IsRequired();

            modelBuilder.HasOne(u => u.QuoteDetail)
            .WithMany(u => u.SubscriptionHistories)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.Member)
            .WithMany(u => u.SubscriptionHistories)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedSubscriptionHistories)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedSubscriptionHistories)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
