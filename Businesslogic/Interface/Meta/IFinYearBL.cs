using BusinessObject;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BusinessLogic.Interface
{
    public interface IFinYearBL
    {
        IEnumerable<SelectListItem> GetLatestFinYearSelectItem();

        IEnumerable<SelectListItem>  GetSelectListItem(bool excludeDefaultItem = false, bool includeUpcomingYear = false);

        int GetCurrentFinYearId();
    }
}
