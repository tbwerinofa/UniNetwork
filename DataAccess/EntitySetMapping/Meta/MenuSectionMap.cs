using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DataAccess.DataMapping
{
    public partial class MenuSectionMap : InsightEntityTypeConfiguration<MenuSection>
    {
        public MenuSectionMap()
        {
            

        }

        public override void Configure(EntityTypeBuilder<MenuSection> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex( a =>new { a.Name, a.MenuGroupId }).IsUnique();
            modelBuilder.Property(a => a.Name).HasMaxLength(50);
            modelBuilder.Property(a => a.MenuGroupId).IsRequired();


            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedMenuSections)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedMenuSections)
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
              .HasOne(u => u.MenuGroup)
              .WithMany(u => u.MenuSections)
              .IsRequired()
              .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
