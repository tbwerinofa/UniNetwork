using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using UniNetworkWeb.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;
 using System.Collections.Generic;
using UniNetworkWeb.Infrastructure;
using Finbuckle.MultiTenant;

namespace UniNetworkWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Register the db context, but do not specify a provider/connection
            // string since these vary by tenant.
            services.AddDbContext<ApplicationDbContext>();

            services.AddDefaultIdentity<IdentityUser>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddRazorPages(options =>
            {
                // Since we are using the route multitenant strategy we must add the
                // route parameter to the Pages conventions used by Identity.
                options.Conventions.AddAreaFolderRouteModelConvention("Identity", "/Account", model =>
                {
                    foreach (var selector in model.Selectors)
                    {
                        selector.AttributeRouteModel.Template =
                            AttributeRouteModel.CombineTemplates("{__tenant__}", selector.AttributeRouteModel.Template);
                    }
                });
            });

            services.DecorateService<LinkGenerator, AmbientValueLinkGenerator>(new List<string> { "__tenant__" });

            services.AddMultiTenant<SampleTenantInfo>()
                    .WithRouteStrategy()
                    .WithConfigurationStore()
                    .WithPerTenantAuthentication();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.EnvironmentName == "Development")
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseMultiTenant();
            //app.UseAuthentication();
            //app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{__tenant__=}/{controller=Home}/{action=Index}");
                endpoints.MapRazorPages();
            });


        }


        private void ConfigRoutes(IRouteBuilder routes)
        {
            routes.MapRoute("Defaut", "{__tenant__=}/{controller=Home}/{action=Index}");
        }
    }
}
