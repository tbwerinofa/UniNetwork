using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{

    public partial class TrainingPlanDistanceMap : InsightEntityTypeConfiguration<TrainingPlanDistance>
    {
        public TrainingPlanDistanceMap()
        {
        }

        public override void Configure(EntityTypeBuilder<TrainingPlanDistance> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex(a => new {a.TrainingPlanId,a.DistanceId }).IsUnique();

            modelBuilder.Property(a => a.TrainingPlanId).IsRequired();
            modelBuilder.Property(a => a.DistanceId).IsRequired();

            modelBuilder
            .HasOne(u => u.TrainingPlan)
            .WithMany(u => u.TrainingPlanDistances)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
            .HasOne(u => u.Distance)
            .WithMany(u => u.TrainingPlanDistances)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedTrainingPlanDistances)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.UpdatedUser)
             .WithMany(u => u.UpdatedTrainingPlanDistances)
             .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
