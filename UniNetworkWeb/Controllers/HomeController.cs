using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UniNetworkWeb.Data;
using UniNetworkWeb.Models;

namespace UniNetworkWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ToDoDbContext dbContext;

        public HomeController(ToDoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            // Get the list of to do items. This will only return items for the current tenant.
            IEnumerable<ToDoItem> toDoItems = null;
            if (HttpContext.GetMultiTenantContext<TenantInfo>()?.TenantInfo != null)
            {
                toDoItems = dbContext.ToDoItems.ToList();
            }

            return View(toDoItems);
        }
    }
}
