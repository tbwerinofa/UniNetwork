using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class ProductMap : InsightEntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
        }

        public override void Configure(EntityTypeBuilder<Product> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex( a =>new { a.Name,a.ProductCategoryId}).IsUnique();
            modelBuilder.Property(a => a.Name).IsRequired();
            modelBuilder.Property(a => a.ProductCategoryId).IsRequired();


            modelBuilder.HasOne(u => u.ProductCategory)
            .WithMany(u => u.Products)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedProducts)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedProducts)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
