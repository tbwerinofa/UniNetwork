using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class ProductSizeMap : InsightEntityTypeConfiguration<ProductSize>
    {
        public ProductSizeMap()
        {
        }

        public override void Configure(EntityTypeBuilder<ProductSize> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex( a =>new { a.SizeId,a.ProductId}).IsUnique();
            modelBuilder.Property(a => a.ProductId).IsRequired();
            modelBuilder.Property(a => a.SizeId).IsRequired();


            modelBuilder.HasOne(u => u.Size)
             .WithMany(u => u.ProductSizes)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.Product)
            .WithMany(u => u.ProductSizes)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedProductSizes)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedProductSizes)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
