using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccess.Infrastructure
{
    public class SharedDesignTimeFactory : IDesignTimeDbContextFactory<SqlServerApplicationDbContext>
    {
        public SqlServerApplicationDbContext CreateDbContext(string[] args)
        {
            // To prep each database uncomment the corresponding line below.
            // var tenantInfo = new TenantInfo { ConnectionString = @"Data Source=(localdb)\\mssqllocaldb;Database=SharedIdentity" };
            var tenantInfo = new TenantInfo { ConnectionString = @"Data Source=DESKTOP-JODV58O\MSSQL14;Database=SharedIdentityReload;Trusted_Connection=True;MultipleActiveResultSets=true" };
            var optionsBuilder = new DbContextOptionsBuilder<SqlServerApplicationDbContext>();

            return new SqlServerApplicationDbContext(tenantInfo, optionsBuilder.Options);
        }
    }
}
