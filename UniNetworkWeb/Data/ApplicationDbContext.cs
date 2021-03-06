﻿using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;

namespace UniNetworkWeb.Data
{
    public class ApplicationDbContext : MultiTenantIdentityDbContext
    {
        public ApplicationDbContext(ITenantInfo tenantInfo) : base(tenantInfo)
        {
        }

        public ApplicationDbContext(ITenantInfo tenantInfo, DbContextOptions<ApplicationDbContext> options)
            : base(tenantInfo, options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(TenantInfo.ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}


