using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class StockAlertMap : InsightEntityTypeConfiguration<StockAlert>
    {
        public StockAlertMap()
        {
        }

        public override void Configure(EntityTypeBuilder<StockAlert> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.Property(a => a.ProductSizeId).IsRequired();
            modelBuilder.Property(a => a.ProductId).IsRequired();

            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedStockAlerts)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedStockAlerts)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
