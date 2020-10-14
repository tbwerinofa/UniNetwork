using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class MenuGroupMap : InsightEntityTypeConfiguration<MenuGroup>
    {
        public MenuGroupMap()
        {
            

        }

        public override void Configure(EntityTypeBuilder<MenuGroup> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex( a =>new { a.Name}).IsUnique();
            modelBuilder.HasIndex(a => new { a.Ordinal }).IsUnique();
            modelBuilder.Property(a => a.Name).HasMaxLength(50);
            modelBuilder.Property(a => a.Name).IsRequired();
            modelBuilder.Property(a => a.Ordinal).IsRequired();
            modelBuilder.Property(a => a.Icon).IsRequired();


            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedMenuGroups)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedMenuGroups)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
