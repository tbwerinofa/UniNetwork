using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{

    public partial class ApplicationRoleMessageTemplateMap : InsightEntityTypeConfiguration<ApplicationRoleMessageTemplate>
    {
        public ApplicationRoleMessageTemplateMap()
        {
        }

        public override void Configure(EntityTypeBuilder<ApplicationRoleMessageTemplate> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex(a => new { a.ApplicationRoleId, a.MessageTemplateId }).IsUnique();
            modelBuilder.Property(a => a.ApplicationRoleId).IsRequired();
            modelBuilder.Property(a => a.MessageTemplateId).IsRequired();

            modelBuilder
            .HasOne(u => u.MessageTemplate)
            .WithMany(u => u.ApplicationRoleMessageTemplates)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
           .HasOne(u => u.ApplicationRole)
           .WithMany(u => u.ApplicationRoleMessageTemplates)
           .IsRequired()
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedApplicationRoleMessageTemplates)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.UpdatedUser)
             .WithMany(u => u.UpdatedApplicationRoleMessageTemplates)
             .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
