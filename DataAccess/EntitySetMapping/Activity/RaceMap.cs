using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{

    public partial class RaceMap : InsightEntityTypeConfiguration<Race>
    {
        public RaceMap()
        {
        }

        public override void Configure(EntityTypeBuilder<Race> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex(a => new {a.FinYearId,a.RaceDefinitionId }).IsUnique();

            modelBuilder.Property(a => a.FinYearId).IsRequired();
            modelBuilder.Property(a => a.RaceDefinitionId).IsRequired();

            modelBuilder
           .HasOne(u => u.RaceDefinition)
           .WithMany(u => u.Races)
           .IsRequired()
           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
           .HasOne(u => u.FinYear)
           .WithMany(u => u.Races)
           .IsRequired()
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedRaces)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.UpdatedUser)
             .WithMany(u => u.UpdatedRaces)
             .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
