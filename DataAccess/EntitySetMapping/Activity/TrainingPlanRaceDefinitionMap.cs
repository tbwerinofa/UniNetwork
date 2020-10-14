using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{

    public partial class TrainingPlanRaceDefinitionMap : InsightEntityTypeConfiguration<TrainingPlanRaceDefinition>
    {
        public TrainingPlanRaceDefinitionMap()
        {
        }

        public override void Configure(EntityTypeBuilder<TrainingPlanRaceDefinition> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex(a => new {a.TrainingPlanId,a.RaceDefinitionId }).IsUnique();

            modelBuilder.Property(a => a.TrainingPlanId).IsRequired();
            modelBuilder.Property(a => a.RaceDefinitionId).IsRequired();

            modelBuilder
            .HasOne(u => u.TrainingPlan)
            .WithMany(u => u.TrainingPlanRaceDefinitions)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
            .HasOne(u => u.RaceDefinition)
            .WithMany(u => u.TrainingPlanRaceDefinitions)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedTrainingPlanRaceDefinitions)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.UpdatedUser)
             .WithMany(u => u.UpdatedTrainingPlanRaceDefinitions)
             .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
