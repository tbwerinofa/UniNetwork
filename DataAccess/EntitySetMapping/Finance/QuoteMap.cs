using DataAccess.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DataMapping
{
    public partial class QuoteMap : InsightEntityTypeConfiguration<Quote>
    {
        public QuoteMap()
        {
        }

        public override void Configure(EntityTypeBuilder<Quote> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex( a =>new { a.QuoteNo }).IsUnique();
            modelBuilder.Property(a => a.QuoteNo).HasMaxLength(50);

            modelBuilder.HasOne(u => u.QuoteStatus)
            .WithMany(u => u.Quotes)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.FinYear)
            .WithMany(u => u.Quotes)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.QuoteUser)
       .WithMany(u => u.Quotes)
       .IsRequired()
       .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedQuotes)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedQuotes)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
