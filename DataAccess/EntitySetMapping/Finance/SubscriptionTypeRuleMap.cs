using DataAccess.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DataMapping
{
    public partial class SubscriptionTypeRuleMap : InsightEntityTypeConfiguration<SubscriptionTypeRule>
    {
        public SubscriptionTypeRuleMap()
        {
        }

        public override void Configure(EntityTypeBuilder<SubscriptionTypeRule> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex( a =>new { a.SubscriptionTypeId}).IsUnique();
            modelBuilder.Property(a => a.SubscriptionTypeId).HasMaxLength(50);

            modelBuilder.HasOne(u => u.SubscriptionType)
           .WithMany(u => u.SubscriptionTypeRules)
           .IsRequired()
           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.HasOne(u => u.AgeGroup)
             .WithMany(u => u.SubscriptionTypeRules)
             .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedSubscriptionTypeRules)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedSubscriptionTypeRules)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
