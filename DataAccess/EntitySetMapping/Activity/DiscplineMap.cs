﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{

    public partial class DiscplineMap : InsightEntityTypeConfiguration<Discpline>
    {
        public DiscplineMap()
        {
        }

        public override void Configure(EntityTypeBuilder<Discpline> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex(a => new { a.Name }).IsUnique();
            modelBuilder.Property(a => a.Name).IsRequired();

            modelBuilder
             .HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedDiscplines)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .HasOne(u => u.UpdatedUser)
             .WithMany(u => u.UpdatedDiscplines)
             .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
