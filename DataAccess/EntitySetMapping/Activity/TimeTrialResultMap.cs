using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{

    public partial class TimeTrialResultMap : InsightEntityTypeConfiguration<TimeTrialResult>
    {
        public TimeTrialResultMap()
        {
        }

        public override void Configure(EntityTypeBuilder<TimeTrialResult> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex(a => new { a.MemberId, a.TimeTrialDistanceId }).IsUnique();


            modelBuilder
          .HasOne(u => u.Member)
          .WithMany(u => u.TimeTrialResults)
          .IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
          .HasOne(u => u.TimeTrialDistance)
          .WithMany(u => u.TimeTrialResults)
          .IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedTimeTrialResults)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.UpdatedUser)
             .WithMany(u => u.UpdatedTimeTrialResults)
             .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
