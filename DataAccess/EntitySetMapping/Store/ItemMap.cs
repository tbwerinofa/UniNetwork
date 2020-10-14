using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class ItemMap : InsightEntityTypeConfiguration<Item>
    {
        public ItemMap()
        {
        }

        public override void Configure(EntityTypeBuilder<Item> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex( a =>new { a.ProductSizeId}).IsUnique();
            modelBuilder.Property(a => a.ProductSizeId).IsRequired();
            modelBuilder.Property(a => a.Quantity).IsRequired();

            modelBuilder.HasOne(u => u.ProductSize)
             .WithMany(u => u.Items)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedItems)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedItems)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
