using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{

    public partial class TimeTrialDistanceMap : InsightEntityTypeConfiguration<TimeTrialDistance>
    {
        public TimeTrialDistanceMap()
        {
        }

        public override void Configure(EntityTypeBuilder<TimeTrialDistance> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex(a => new { a.TimeTrialId, a.DistanceId }).IsUnique();

            modelBuilder.Property(a => a.TimeTrialId).IsRequired();
            modelBuilder.Property(a => a.DistanceId).IsRequired();

            modelBuilder
            .HasOne(u => u.TimeTrial)
            .WithMany(u => u.TimeTrialDistances)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
            .HasOne(u => u.Distance)
            .WithMany(u => u.TimeTrialDistances)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedTimeTrialDistances)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.UpdatedUser)
             .WithMany(u => u.UpdatedTimeTrialDistances)
             .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
