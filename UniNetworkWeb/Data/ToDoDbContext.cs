using UniNetworkWeb.Models;
using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UniNetworkWeb.Data
{
    public class ToDoDbContext : MultiTenantDbContext
    {
        public ToDoDbContext(ITenantInfo tenantInfo) : base(tenantInfo)
        {
        }

        public ToDoDbContext(ITenantInfo tenantInfo, DbContextOptions<ToDoDbContext> options) : base(tenantInfo, options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(TenantInfo.ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoItem>().IsMultiTenant();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}