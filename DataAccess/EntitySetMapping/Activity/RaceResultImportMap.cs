using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{

    public partial class RaceResultImportMap : InsightEntityTypeConfiguration<RaceResultImport>
    {
        public RaceResultImportMap()
        {
        }

        public override void Configure(EntityTypeBuilder<RaceResultImport> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex(a => new {a.Discriminator }).IsUnique();
            modelBuilder.Property(a => a.RaceDefinition).IsRequired();
            modelBuilder.Property(a => a.RaceType).IsRequired();

            modelBuilder
          .HasOne(u => u.Person)
          .WithMany(u => u.RaceResultImports)
          .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
        .HasOne(u => u.RaceDistance)
        .WithMany(u => u.RaceResultImports)
        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedRaceResultImports)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.UpdatedUser)
             .WithMany(u => u.UpdatedRaceResultImports)
             .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
