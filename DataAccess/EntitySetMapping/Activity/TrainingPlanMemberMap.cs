using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{

    public partial class TrainingPlanMemberMap : InsightEntityTypeConfiguration<TrainingPlanMember>
    {
        public TrainingPlanMemberMap()
        {
        }

        public override void Configure(EntityTypeBuilder<TrainingPlanMember> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex(a => new {a.TrainingPlanId,a.MemberId }).IsUnique();

            modelBuilder.Property(a => a.TrainingPlanId).IsRequired();
            modelBuilder.Property(a => a.MemberId).IsRequired();

            modelBuilder
            .HasOne(u => u.TrainingPlan)
            .WithMany(u => u.TrainingPlanMembers)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
            .HasOne(u => u.Member)
            .WithMany(u => u.TrainingPlanMembers)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedTrainingPlanMembers)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.UpdatedUser)
             .WithMany(u => u.UpdatedTrainingPlanMembers)
             .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
