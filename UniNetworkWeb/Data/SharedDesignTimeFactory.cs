using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
namespace UniNetworkWeb.Data
{
    public class SharedDesignTimeFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // To prep each database uncomment the corresponding line below.
           // var tenantInfo = new TenantInfo { ConnectionString = @"Data Source=(localdb)\\mssqllocaldb;Database=SharedIdentity" };
            var tenantInfo = new TenantInfo{ ConnectionString = @"Data Source=DESKTOP-JODV58O\MSSQL14;Database=SharedIdentity;Trusted_Connection=True;MultipleActiveResultSets=true" };
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            return new ApplicationDbContext(tenantInfo, optionsBuilder.Options);
        }
    }
}

