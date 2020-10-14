using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class FinYearCycleMap : InsightEntityTypeConfiguration<FinYearCycle>
    {
        public FinYearCycleMap()
        {
        }

        public override void Configure(EntityTypeBuilder<FinYearCycle> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex( a =>new { a.CycleId,a.FinYearId}).IsUnique();
            modelBuilder.Property(a => a.CycleId).IsRequired();
            modelBuilder.Property(a => a.FinYearId).IsRequired();

            modelBuilder.HasOne(u => u.Cycle)
             .WithMany(u => u.FinYearCycles)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.FinYear)
            .WithMany(u => u.FinYearCycles)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedFinYearCycles)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedFinYearCycles)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
