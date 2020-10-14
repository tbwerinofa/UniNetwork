using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{

    public partial class RaceOrganisationMap : InsightEntityTypeConfiguration<RaceOrganisation>
    {
        public RaceOrganisationMap()
        {
        }

        public override void Configure(EntityTypeBuilder<RaceOrganisation> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex(a => new {a.RaceId, a.OrganisationId }).IsUnique();
            modelBuilder.Property(a => a.OrganisationId).IsRequired();
            modelBuilder.Property(a => a.RaceId).IsRequired();

            modelBuilder
            .HasOne(u => u.Race)
            .WithMany(u => u.RaceOrganisations)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
            .HasOne(u => u.Organisation)
            .WithMany(u => u.RaceOrganisations)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);


            modelBuilder
             .HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedRaceOrganisations)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.UpdatedUser)
             .WithMany(u => u.UpdatedRaceOrganisations)
             .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
