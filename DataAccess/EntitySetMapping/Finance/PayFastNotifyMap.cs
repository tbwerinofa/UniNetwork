using DataAccess.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DataMapping
{
    public partial class PayFastNotifyMap : InsightEntityTypeConfiguration<PayFastNotify>
    {
        public PayFastNotifyMap()
        {
        }

        public override void Configure(EntityTypeBuilder<PayFastNotify> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            
            modelBuilder.HasOne(u => u.Quote)
            .WithMany(u => u.PayFastNotifies)
            .IsRequired()
            .HasForeignKey(u=>u.Custom_int1)
            .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
