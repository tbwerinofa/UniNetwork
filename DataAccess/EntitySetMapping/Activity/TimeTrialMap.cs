using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{

    public partial class TimeTrialMap : InsightEntityTypeConfiguration<TimeTrial>
    {
        public TimeTrialMap()
        {
        }

        public override void Configure(EntityTypeBuilder<TimeTrial> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex(a => new { a.RaceTypeId, a.CalendarId }).IsUnique();

            modelBuilder.Property(a => a.RaceTypeId).IsRequired();
            modelBuilder.Property(a => a.CalendarId).IsRequired();

            modelBuilder
           .HasOne(u => u.Calendar)
           .WithMany(u => u.TimeTrials)
           .IsRequired()
           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
           .HasOne(u => u.RaceType)
           .WithMany(u => u.TimeTrials)
           .IsRequired()
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedTimeTrials)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.UpdatedUser)
             .WithMany(u => u.UpdatedTimeTrials)
             .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
