using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{

    public partial class TrainingPlanMap : InsightEntityTypeConfiguration<TrainingPlan>
    {
        public TrainingPlanMap()
        {
        }

        public override void Configure(EntityTypeBuilder<TrainingPlan> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex(a => new {a.Name,a.FinYearId }).IsUnique();

            modelBuilder.Property(a => a.Name).IsRequired();
            modelBuilder.Property(a => a.FinYearId).IsRequired();

            modelBuilder
           .HasOne(u => u.Event)
           .WithMany(u => u.TrainingPlans)
           .IsRequired()
           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
           .HasOne(u => u.FinYear)
           .WithMany(u => u.TrainingPlans)
           .IsRequired()
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedTrainingPlans)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.UpdatedUser)
             .WithMany(u => u.UpdatedTrainingPlans)
             .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
