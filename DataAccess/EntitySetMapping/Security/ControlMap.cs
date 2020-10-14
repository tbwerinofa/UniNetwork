using DataAccess.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DataMapping
{
   
    public partial class ControlMap : InsightEntityTypeConfiguration<Control>
    {
        public ControlMap()
        {


        }

        public override void Configure(EntityTypeBuilder<Control> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex(a => new { a.Type }).IsUnique();
            modelBuilder.Property(a => a.Type).IsRequired();
            modelBuilder.Property(a => a.Value).IsRequired();


            modelBuilder
             .HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedControls)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);


            modelBuilder
             .HasOne(u => u.UpdatedUser)
             .WithMany(u => u.UpdatedControls)
             .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
