using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class FeaturedImageMap : InsightEntityTypeConfiguration<FeaturedImage>
    {
        public FeaturedImageMap()
        {
        }

        public override void Configure(EntityTypeBuilder<FeaturedImage> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex( a =>new { a.FeaturedCategoryId,a.ProductImageId}).IsUnique();
            modelBuilder.Property(a => a.FeaturedCategoryId).IsRequired();
            modelBuilder.Property(a => a.ProductImageId).IsRequired();

            modelBuilder.HasOne(u => u.ProductImage)
             .WithMany(u => u.FeaturedImages)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.FeaturedCategory)
            .WithMany(u => u.FeaturedImages)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedFeaturedImages)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedFeaturedImages)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
