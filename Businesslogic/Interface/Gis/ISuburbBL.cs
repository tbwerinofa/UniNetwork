using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BusinessLogic.Interface
{
    public interface ISuburbBL
    {
        IEnumerable<SelectListItem> GetEntitySelectListItem_ByParentId(
            int parentId);

    }
}
