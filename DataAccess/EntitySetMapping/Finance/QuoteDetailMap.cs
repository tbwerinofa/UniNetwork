using DataAccess.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DataMapping
{
    public partial class QuoteDetailMap : InsightEntityTypeConfiguration<QuoteDetail>
    {
        public QuoteDetailMap()
        {
        }

        public override void Configure(EntityTypeBuilder<QuoteDetail> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex( a =>new { a.QuoteId,a.ItemNo,a.SubscriptionTypeRuleAuditId }).IsUnique();
            modelBuilder.Property(a => a.QuoteId).IsRequired();
            modelBuilder.Property(a => a.ItemNo).IsRequired();
            modelBuilder.Property(a => a.SubscriptionTypeRuleAuditId).IsRequired();
            modelBuilder.Property(a => a.Quantity).IsRequired();

            modelBuilder.HasOne(u => u.Quote)
            .WithMany(u => u.QuoteDetails)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.HasOne(u => u.SubscriptionTypeRuleAudit)
            .WithMany(u => u.QuoteDetails)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedQuoteDetails)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedQuoteDetails)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
