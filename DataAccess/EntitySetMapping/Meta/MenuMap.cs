using DataAccess.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DataMapping
{
    [Table(nameof(Menu), Schema = SchemaName.Meta)]
    public partial class MenuMap : InsightEntityTypeConfiguration<Menu>
    {
        public MenuMap()
        {
            

        }

        public override void Configure(EntityTypeBuilder<Menu> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex( a =>new { a.Controller, a.ActionResult }).IsUnique();
            modelBuilder.HasIndex(a => new { a.MenuSectionId, a.Ordinal }).IsUnique();
            modelBuilder.Property(a => a.Name).HasMaxLength(50);
            modelBuilder.Property(a => a.Controller).HasMaxLength(50);
            modelBuilder.Property(a => a.ActionResult).HasMaxLength(50);
            modelBuilder.Property(a => a.Name).IsRequired();
            modelBuilder.Property(a => a.Controller).IsRequired();
            modelBuilder.Property(a => a.ActionResult).IsRequired();
            modelBuilder.Property(a => a.MenuSectionId).IsRequired();
            modelBuilder.Property(a => a.Ordinal).IsRequired();

            modelBuilder
          .Property(b => b.HasArea)
          .HasDefaultValue(1);

            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedMenus)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedMenus)
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
              .HasOne(u => u.MenuSection)
              .WithMany(u => u.Menus)
              .IsRequired()
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
            .HasOne(u => u.MenuArea)
            .WithMany(u => u.Menus)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);



        }
    }
}
