using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class SuburbMap : InsightEntityTypeConfiguration<Suburb>
    {
        public SuburbMap()
        {
            

        }

        public override void Configure(EntityTypeBuilder<Suburb> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");



            modelBuilder.Property(a => a.TownId).IsRequired();
            modelBuilder.Property(a => a.Name).IsRequired();


            modelBuilder
                     .HasOne(u => u.Town)
                     .WithMany(u => u.Suburbs)
                     .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedSuburbs)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedSuburbs)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
