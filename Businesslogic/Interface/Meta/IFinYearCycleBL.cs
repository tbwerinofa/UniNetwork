using DataAccess;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Interface
{
    public interface IFinYearCycleBL
    {

        IEnumerable<SelectListItem> GetSelectListItem(int? parentID);

        IQueryable<FinYearCycle> GetLatestEntityList();

        FinYearCycle GetLatestEntity();

    }
}
