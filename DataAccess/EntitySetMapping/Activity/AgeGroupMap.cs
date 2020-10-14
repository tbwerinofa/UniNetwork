using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{

    public partial class AgeGroupMap : InsightEntityTypeConfiguration<AgeGroup>
    {
        public AgeGroupMap()
        {
        }

        public override void Configure(EntityTypeBuilder<AgeGroup> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex(a => new {a.MinValue }).IsUnique();
            modelBuilder.HasIndex(a => new { a.MaxValue }).IsUnique();
            modelBuilder.HasIndex(a => new { a.Name }).IsUnique();
            modelBuilder.Property(a => a.MinValue).IsRequired();
            modelBuilder.Property(a => a.MaxValue).IsRequired();
            modelBuilder.Property(a => a.Name).IsRequired();


            modelBuilder
             .HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedAgeGroups)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.UpdatedUser)
             .WithMany(u => u.UpdatedAgeGroups)
             .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
