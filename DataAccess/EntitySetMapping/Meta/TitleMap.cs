using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class TitleMap : InsightEntityTypeConfiguration<Title>
    {
        public TitleMap()
        {
            

        }

        public override void Configure(EntityTypeBuilder<Title> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex( a =>new { a.Name}).IsUnique();
            modelBuilder.Property(a => a.Name).HasMaxLength(50);


            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedTitles)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedTitles)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
