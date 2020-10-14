using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{

    public partial class DistanceMap : InsightEntityTypeConfiguration<Distance>
    {
        public DistanceMap()
        {


        }

        public override void Configure(EntityTypeBuilder<Distance> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex(a => new { a.Name, a.Measurement,a.MeasurementUnitId }).IsUnique();
            modelBuilder.Property(a => a.Name).IsRequired();
            modelBuilder.Property(a => a.Measurement).IsRequired();
            modelBuilder.Property(a => a.MeasurementUnitId).IsRequired();

            modelBuilder
          .HasOne(u => u.MeasurementUnit)
          .WithMany(u => u.Distances)
          .IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedDistances)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.UpdatedUser)
             .WithMany(u => u.UpdatedDistances)
             .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
