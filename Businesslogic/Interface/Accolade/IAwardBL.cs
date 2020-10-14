using BusinessObject;
using BusinessObject.Component;
using BusinessObject.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interface
{
    public interface IAwardBL
    {
        IEnumerable<SelectListItem> GetSelectListItems();
        IEnumerable<SelectListItem> GetSelectListItems_byFrequencyId(int frequncyId);
    }
}
