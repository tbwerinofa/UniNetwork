using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{

    public partial class RaceResultMap : InsightEntityTypeConfiguration<RaceResult>
    {
        public RaceResultMap()
        {
        }

        public override void Configure(EntityTypeBuilder<RaceResult> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex(a => new {a.MemberId,a.RaceDistanceId }).IsUnique();
            modelBuilder.Property(a => a.MemberId).IsRequired();
            modelBuilder.Property(a => a.RaceDistanceId).IsRequired();

            modelBuilder
             .HasOne(u => u.AgeGroup)
             .WithMany(u => u.RaceResults)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
          .HasOne(u => u.Member)
          .WithMany(u => u.RaceResults)
          .IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
          .HasOne(u => u.RaceDistance)
          .WithMany(u => u.RaceResults)
          .IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedRaceResults)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.UpdatedUser)
             .WithMany(u => u.UpdatedRaceResults)
             .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
