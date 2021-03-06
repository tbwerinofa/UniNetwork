﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class MemberMappingMap : InsightEntityTypeConfiguration<MemberMapping>
    {
        public MemberMappingMap()
        {
        }

        public override void Configure(EntityTypeBuilder<MemberMapping> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex( a =>new {a.MemberId,a.RelationMemberId }).IsUnique();

            modelBuilder.Property(a => a.MemberId).IsRequired();
            modelBuilder.Property(a => a.RelationMemberId).IsRequired();

            modelBuilder.HasOne(u => u.Member)
            .WithMany(u => u.MemberMappings)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.RelationMember)
            .WithMany(u => u.RelationMemberMappings)
            .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.HasOne(u => u.CreatedUser)
             .WithMany(u => u.CreatedMemberMappings)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedMemberMappings)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
