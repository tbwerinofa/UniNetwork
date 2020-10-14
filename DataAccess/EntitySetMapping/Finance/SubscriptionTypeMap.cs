using DataAccess.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DataMapping
{
    public partial class SubscriptionTypeMap : InsightEntityTypeConfiguration<SubscriptionType>
    {
        public SubscriptionTypeMap()
        {
        }

        public override void Configure(EntityTypeBuilder<SubscriptionType> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex( a =>new { a.Name}).IsUnique();
            modelBuilder.Property(a => a.Name).HasMaxLength(50);


            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedSubscriptionTypes)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedSubscriptionTypes)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
