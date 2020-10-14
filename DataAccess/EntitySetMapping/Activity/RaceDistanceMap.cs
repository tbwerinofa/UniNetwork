using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{

    public partial class RaceDistanceMap : InsightEntityTypeConfiguration<RaceDistance>
    {
        public RaceDistanceMap()
        {
        }

        public override void Configure(EntityTypeBuilder<RaceDistance> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex(a => new {a.RaceId,a.DistanceId }).IsUnique();

            modelBuilder.Property(a => a.RaceId).IsRequired();
            modelBuilder.Property(a => a.DistanceId).IsRequired();
            modelBuilder.Property(a => a.EventDate).IsRequired();

            modelBuilder
            .HasOne(u => u.Race)
            .WithMany(u => u.RaceDistances)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
            .HasOne(u => u.Distance)
            .WithMany(u => u.RaceDistances)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedRaceDistances)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.UpdatedUser)
             .WithMany(u => u.UpdatedRaceDistances)
             .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
