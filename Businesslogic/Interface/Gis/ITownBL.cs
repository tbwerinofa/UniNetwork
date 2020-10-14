using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BusinessLogic.Interface
{
    public interface ITownBL
    {
        IEnumerable<SelectListItem> GetEntitySelectListItem_ByParentId(
            int parentId);

    }
}
