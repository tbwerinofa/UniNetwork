using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{
    public partial class ArticleMap : InsightEntityTypeConfiguration<Article>
    {
        public ArticleMap()
        {
        }

        public override void Configure(EntityTypeBuilder<Article> modelBuilder)
        {
            modelBuilder.HasKey(c => c.Id);
               modelBuilder.HasQueryFilter(p => p.IsActive).Property(b => b.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.Property(a => a.CalendarMonthId).IsRequired();
            modelBuilder.Property(a => a.FinYearId).IsRequired();

            modelBuilder.HasIndex(a => new { a.FinYearId, a.CalendarMonthId,a.PublishDate,a.Name }).IsUnique();
            modelBuilder.Property(a => a.Author).HasMaxLength(100);
            modelBuilder.Property(a => a.Name).HasMaxLength(100);


            modelBuilder
                 .HasOne(u => u.CreatedUser)
                 .WithMany(u => u.CreatedArticles)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
           .HasOne(u => u.UpdatedUser)
           .WithMany(u => u.UpdatedArticles)
           .OnDelete(DeleteBehavior.Restrict);
        }
}

}
