using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class PersonMap : InsightEntityTypeConfiguration<Person>
    {
        public PersonMap()
        {
            

        }

        public override void Configure(EntityTypeBuilder<Person> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");



            modelBuilder.HasIndex( a =>new {a.IDNumber,a.IDTypeId,a.CountryId }).IsUnique();
                  modelBuilder.Property(a => a.IDNumber).HasMaxLength(13);
            modelBuilder.Property(a => a.FirstName).IsRequired();
            modelBuilder.Property(a => a.Surname).IsRequired();
            modelBuilder.Property(a => a.GenderId).IsRequired();

            modelBuilder.Property(a => a.TitleId).IsRequired();
            modelBuilder.Property(a => a.IDTypeId).IsRequired();
            modelBuilder.Property(a => a.CountryId).IsRequired();
            modelBuilder.Property(a => a.PersonGuid).IsRequired();

            modelBuilder
                     .HasOne(u => u.AgeGroup)
                     .WithMany(u => u.People)
                     .IsRequired()
                     .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                 .HasOne(u => u.Document)
                 .WithMany(u => u.People)
                  .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.Document)
             .WithMany(u => u.People)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedPersons)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatePersons)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
