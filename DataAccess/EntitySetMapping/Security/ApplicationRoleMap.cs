using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DataMapping
{

    public partial class ApplicationRoleMap : InsightEntityTypeConfiguration<ApplicationRole>
    {
        public ApplicationRoleMap()
        {
        }

        public override void Configure(EntityTypeBuilder<ApplicationRole> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
           

        }
    }
}
