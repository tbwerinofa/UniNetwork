using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class EthnicMap : InsightEntityTypeConfiguration<Ethnic>
    {
        public EthnicMap()
        {
            

        }

        public override void Configure(EntityTypeBuilder<Ethnic> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex( a =>new { a.Name}).IsUnique();
            modelBuilder.Property(a => a.Name).HasMaxLength(50);


            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedEthnics)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedEthnics)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
