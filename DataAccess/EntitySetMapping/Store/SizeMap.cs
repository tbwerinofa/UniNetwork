using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class SizeMap : InsightEntityTypeConfiguration<Size>
    {
        public SizeMap()
        {
        }

        public override void Configure(EntityTypeBuilder<Size> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex( a =>new { a.Name}).IsUnique();
            modelBuilder.HasIndex(a => new { a.ShortName }).IsUnique();
            modelBuilder.HasIndex(a => new { a.Ordinal }).IsUnique();
            modelBuilder.Property(a => a.Name).IsRequired();
            modelBuilder.Property(a => a.ShortName).IsRequired();
            modelBuilder.Property(a => a.Ordinal).IsRequired();

            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedSizes)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedSizes)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
