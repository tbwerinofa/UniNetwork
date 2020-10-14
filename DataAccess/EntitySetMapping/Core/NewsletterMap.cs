using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class NewsletterMap : InsightEntityTypeConfiguration<Newsletter>
    {
        public NewsletterMap()
        {
        }

        public override void Configure(EntityTypeBuilder<Newsletter> modelBuilder)
        {
            modelBuilder.HasKey(c => c.Id);
               modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.Property(a => a.IssueNo).IsRequired();
            modelBuilder.Property(a => a.ArticleId).IsRequired();

            modelBuilder.HasIndex(a => new { a.IssueNo }).IsUnique();
            modelBuilder.HasIndex(a => new { a.ArticleId }).IsUnique();

            modelBuilder
                 .HasOne(u => u.Article)
                 .WithMany(u => u.Newsletters)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                 .HasOne(u => u.CreatedUser)
                 .WithMany(u => u.CreatedNewsletters)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
           .HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedNewsletters)
           .OnDelete(DeleteBehavior.Restrict);
        }
}

}
