using DataAccess.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DataMapping
{
    public partial class SubscriptionMap : InsightEntityTypeConfiguration<Subscription>
    {
        public SubscriptionMap()
        {
        }

        public override void Configure(EntityTypeBuilder<Subscription> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex( a =>new { a.QuoteDetailId }).IsUnique();
            modelBuilder.Property(a => a.StartDate).IsRequired();

            modelBuilder.HasOne(u => u.QuoteDetail)
            .WithMany(u => u.Subscriptions)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.Member)
            .WithMany(u => u.Subscriptions)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedSubscriptions)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedSubscriptions)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
