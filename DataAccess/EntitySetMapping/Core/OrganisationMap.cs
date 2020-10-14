using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class OrganisationMap : InsightEntityTypeConfiguration<Organisation>
    {
        public OrganisationMap()
        {
        }

        public override void Configure(EntityTypeBuilder<Organisation> modelBuilder)
        {
            modelBuilder.HasKey(c => c.Id);
               modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.Property(a => a.OrganisationTypeId).IsRequired();
            modelBuilder.Property(a => a.Name).IsRequired();

            modelBuilder.HasIndex(a => new { a.Name,a.OrganisationTypeId }).IsUnique();
            modelBuilder.Property(a => a.Name).HasMaxLength(250);

            modelBuilder
          .HasOne(u => u.OrganisationType)
          .WithMany(u => u.Organisations)
          .IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                 .HasOne(u => u.CreatedUser)
                 .WithMany(u => u.CreatedOrganisations)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
           .HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedOrganisations)
           .OnDelete(DeleteBehavior.Restrict);
        }
}

}
