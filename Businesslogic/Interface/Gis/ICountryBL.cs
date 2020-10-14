using BusinessObject.Component;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BusinessLogic.Interface
{
    public interface ICountryBL
    {
        IEnumerable<SelectListItem> GetEntitySelectListItem_ByParentId(
            int parentId);

        IEnumerable<DropDownListItems> GetEntityDropDownListItems();

    }
}
