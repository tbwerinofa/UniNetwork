using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{

    public partial class RaceDefinitionMap : InsightEntityTypeConfiguration<RaceDefinition>
    {
        public RaceDefinitionMap()
        {
        }

        public override void Configure(EntityTypeBuilder<RaceDefinition> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex(a => new { a.Name, a.ProvinceId }).IsUnique();
            modelBuilder.Property(a => a.Name).IsRequired();
            modelBuilder.Property(a => a.ProvinceId).IsRequired();
            modelBuilder.Property(a => a.DiscplineId).IsRequired();
            modelBuilder.Property(a => a.RaceTypeId).IsRequired();


            modelBuilder
          .HasOne(u => u.Province)
          .WithMany(u => u.RaceDefinitions)
          .IsRequired()
          .OnDelete(DeleteBehavior.Restrict);


            modelBuilder
              .HasOne(u => u.Discpline)
              .WithMany(u => u.RaceDefinitions)
              .IsRequired()
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
              .HasOne(u => u.RaceType)
              .WithMany(u => u.RaceDefinitions)
              .IsRequired()
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedRaceDefinitions)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.UpdatedUser)
             .WithMany(u => u.UpdatedRaceDefinitions)
             .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
