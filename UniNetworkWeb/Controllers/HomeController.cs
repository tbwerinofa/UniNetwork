using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Mvc;

namespace UniNetworkWeb.Controllers
{

    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            if (User.Identity.IsAuthenticated)
                 return RedirectToAction("Index","Profile");

            var ti = HttpContext.GetMultiTenantContext<SampleTenantInfo>()?.TenantInfo;
            return View(ti);
        }

        public IActionResult ReturnChallenge()
        {
            return Challenge();
        }

        public IActionResult ReturnForbid()
        {
            return Forbid();
        }
    }

    //public class HomeLegacyController : Controller
    //{
    //    private readonly ToDoDbContext dbContext;

    //    public HomeLegacyController(ToDoDbContext dbContext)
    //    {
    //        this.dbContext = dbContext;
    //    }

    //    public IActionResult Index()
    //    {
    //        // Get the list of to do items. This will only return items for the current tenant.
    //        IEnumerable<ToDoItem> toDoItems = null;
    //        if (HttpContext.GetMultiTenantContext<TenantInfo>()?.TenantInfo != null)
    //        {
    //            toDoItems = dbContext.ToDoItems.ToList();
    //        }

    //        return View(toDoItems);
    //    }
    //}
}
