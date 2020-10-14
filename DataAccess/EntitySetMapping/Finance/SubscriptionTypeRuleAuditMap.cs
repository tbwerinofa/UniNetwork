using DataAccess.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DataMapping
{
    public partial class SubscriptionTypeRuleAuditMap : InsightEntityTypeConfiguration<SubscriptionTypeRuleAudit>
    {
        public SubscriptionTypeRuleAuditMap()
        {
        }

        public override void Configure(EntityTypeBuilder<SubscriptionTypeRuleAudit> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex( a =>new { a.SubscriptionTypeRuleId}).IsUnique();
            modelBuilder.Property(a => a.SubscriptionTypeRuleId).HasMaxLength(50);

            modelBuilder.HasOne(u => u.SubscriptionTypeRule)
           .WithMany(u => u.SubscriptionTypeRuleAudits)
           .IsRequired()
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.AgeGroup)
             .WithMany(u => u.SubscriptionTypeRuleAudits)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedSubscriptionTypeRuleAudits)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedSubscriptionTypeRuleAudits)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
