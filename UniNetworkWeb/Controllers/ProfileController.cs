using BusinessObject;
using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace UniNetworkWeb.Controllers
{

    [Authorize]
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            IEnumerable<SelectListItem> emptySelectListItem = Enumerable.Empty<SelectListItem>();
            ProfileViewModel model = new ProfileViewModel {
                Countries = emptySelectListItem,
                Address = new BusinessObject.AddressViewModel
                {
                    Countries = emptySelectListItem,
                    Cities = emptySelectListItem,
                    Provinces = emptySelectListItem,
                    Towns = emptySelectListItem,
                    Suburbs = emptySelectListItem,
                }
            };
            return View(model);
        }
    }
}
