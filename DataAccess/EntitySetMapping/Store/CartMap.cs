using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class CartMap : InsightEntityTypeConfiguration<Cart>
    {
        public CartMap()
        {
        }

        public override void Configure(EntityTypeBuilder<Cart> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex( a =>new { a.ProductSizeId}).IsUnique();
            modelBuilder.Property(a => a.ProductSizeId).IsRequired();
            modelBuilder.Property(a => a.Count).IsRequired();

            modelBuilder.HasOne(u => u.ProductSize)
             .WithMany(u => u.Carts)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedCarts)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedCarts)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
