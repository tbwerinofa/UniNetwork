using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class GenderMap : InsightEntityTypeConfiguration<Gender>
    {
        public GenderMap()
        {
            

        }

        public override void Configure(EntityTypeBuilder<Gender> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");
            modelBuilder.HasIndex( a =>new { a.Name}).IsUnique();
            modelBuilder.HasIndex(a => new { a.Discriminator }).IsUnique();
            modelBuilder.Property(a => a.Name).HasMaxLength(50);
            modelBuilder.Property(a => a.Discriminator).HasMaxLength(50);
            modelBuilder.Property(a => a.Name).IsRequired();
            modelBuilder.Property(a => a.Discriminator).IsRequired();


            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedGenders)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedGenders)
           .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
