using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class MemberStagingMap : InsightEntityTypeConfiguration<MemberStaging>
    {
        public MemberStagingMap()
        {
            

        }

        public override void Configure(EntityTypeBuilder<MemberStaging> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex( a =>new {a.IDNumber,a.IDTypeId,a.CountryId }).IsUnique();
            modelBuilder.Property(a => a.IDNumber).HasMaxLength(13);
            modelBuilder.Property(a => a.BirthDate).IsRequired();
            modelBuilder.Property(a => a.FirstName).IsRequired();
            modelBuilder.Property(a => a.Surname).IsRequired();
            modelBuilder.Property(a => a.GenderId).IsRequired();
            modelBuilder.Property(a => a.IDNumber).IsRequired();
            modelBuilder.Property(a => a.IDTypeId).IsRequired();
            modelBuilder.Property(a => a.CountryId).IsRequired();

            modelBuilder.Property(a => a.Comment).HasMaxLength(50);

            modelBuilder.HasOne(u => u.Title)
        .WithMany(u => u.MemberStagings)
        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.Address)
            .WithMany(u => u.MemberStagings)
            .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.HasOne(u => u.Gender)
        .WithMany(u => u.MemberStagings)
        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.IDType)
        .WithMany(u => u.MemberStagings)
        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.Country)
        .WithMany(u => u.MemberStagings)
        .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedMemberStagings)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedMemberStagings)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
