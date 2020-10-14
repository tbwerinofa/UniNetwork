using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class ProductImageMap : InsightEntityTypeConfiguration<ProductImage>
    {
        public ProductImageMap()
        {
        }

        public override void Configure(EntityTypeBuilder<ProductImage> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex( a =>new { a.ProductId,a.DocumentId}).IsUnique();
            modelBuilder.Property(a => a.ProductId).IsRequired();
            modelBuilder.Property(a => a.DocumentId).IsRequired();

            modelBuilder.HasOne(u => u.Document)
             .WithMany(u => u.ProductImages)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.Product)
            .WithMany(u => u.ProductImages)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedProductImages)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedProductImages)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
