using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{

    public partial class MeasurementUnitMap : InsightEntityTypeConfiguration<MeasurementUnit>
    {
        public MeasurementUnitMap()
        {
        }

        public override void Configure(EntityTypeBuilder<MeasurementUnit> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");


            modelBuilder.HasIndex(a => new {a.Name }).IsUnique();
            modelBuilder.Property(a => a.Name).IsRequired();

            modelBuilder
             .HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedMeasurementUnits)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.UpdatedUser)
             .WithMany(u => u.UpdatedMeasurementUnits)
             .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
