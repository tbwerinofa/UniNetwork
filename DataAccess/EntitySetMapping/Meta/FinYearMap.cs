using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class FinYearMap : InsightEntityTypeConfiguration<FinYear>
    {
        public FinYearMap()
        {
            

        }

        public override void Configure(EntityTypeBuilder<FinYear> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex( a =>new { a.Name}).IsUnique();
            modelBuilder.HasIndex(a => new { a.StartDate }).IsUnique();
            modelBuilder.HasIndex(a => new { a.EndDate }).IsUnique();
            modelBuilder.Property(a => a.Name).HasMaxLength(50);
            modelBuilder.Property(a => a.Name).IsRequired();
            modelBuilder.Property(a => a.StartDate).IsRequired();
            modelBuilder.Property(a => a.EndDate).IsRequired();


            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedFinYears)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedFinYears)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
