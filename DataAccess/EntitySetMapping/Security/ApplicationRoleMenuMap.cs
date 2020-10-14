using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{

    public partial class ApplicationRoleMenuMap : InsightEntityTypeConfiguration<ApplicationRoleMenu>
    {
        public ApplicationRoleMenuMap()
        {
        }

        public override void Configure(EntityTypeBuilder<ApplicationRoleMenu> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex(a => new { a.ApplicationRoleId, a.MenuId }).IsUnique();
            modelBuilder.Property(a => a.ApplicationRoleId).IsRequired();
            modelBuilder.Property(a => a.MenuId).IsRequired();

            modelBuilder
            .HasOne(u => u.Menu)
            .WithMany(u => u.ApplicationRoleMenus)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
           .HasOne(u => u.ApplicationRole)
           .WithMany(u => u.ApplicationRoleMenus)
           .IsRequired()
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedApplicationRoleMenus)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.UpdatedUser)
             .WithMany(u => u.UpdatedApplicationRoleMenus)
             .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
