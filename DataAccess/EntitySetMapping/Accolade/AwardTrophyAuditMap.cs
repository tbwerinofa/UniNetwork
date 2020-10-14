using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class AwardTrophyAuditMap : InsightEntityTypeConfiguration<AwardTrophyAudit>
    {
        public AwardTrophyAuditMap()
        {
            

        }

        public override void Configure(EntityTypeBuilder<AwardTrophyAudit> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex( a =>new {a.FinYearId, a.AwardTrophyId,a.StartDate }).IsUnique();
            modelBuilder.Property(a => a.FinYearId).IsRequired();
            modelBuilder.Property(a => a.AwardTrophyId).IsRequired();
            modelBuilder.Property(a => a.StartDate).IsRequired();
 

            modelBuilder
                     .HasOne(u => u.FinYear)
                     .WithMany(u => u.AwardTrophyAudits)
                     .IsRequired()
                     .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.AwardTrophy)
             .WithMany(u => u.AwardTrophyAudits)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedAwardTrophyAudits)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedAwardTrophyAudits)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
