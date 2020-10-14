using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class AwardTrophyMap : InsightEntityTypeConfiguration<AwardTrophy>
    {
        public AwardTrophyMap()
        {
        }

        public override void Configure(EntityTypeBuilder<AwardTrophy> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex( a =>new {a.AwardId, a.FinYearId }).IsUnique();
            modelBuilder.Property(a => a.AwardId).IsRequired();
            modelBuilder.Property(a => a.FinYearId).IsRequired();
            modelBuilder.Property(a => a.StartDate).IsRequired();

            modelBuilder
            .HasOne(u => u.Award)
            .WithMany(u => u.AwardTrophies)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.FinYear)
             .WithMany(u => u.AwardTrophies)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedAwardTrophies)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedAwardTrophies)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
