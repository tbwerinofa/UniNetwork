using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class OrganisationTypeMap : InsightEntityTypeConfiguration<OrganisationType>
    {
        public OrganisationTypeMap()
        {
        }

        public override void Configure(EntityTypeBuilder<OrganisationType> modelBuilder)
        {
            modelBuilder.HasKey(c => c.Id);
               modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.Property(a => a.Name).IsRequired();

            modelBuilder.HasIndex(a => new { a.Name}).IsUnique();
            modelBuilder.Property(a => a.Name).HasMaxLength(250);

            modelBuilder
                 .HasOne(u => u.CreatedUser)
                 .WithMany(u => u.CreatedOrganisationTypes)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
           .HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedOrganisationTypes)
           .OnDelete(DeleteBehavior.Restrict);
        }
}

}
