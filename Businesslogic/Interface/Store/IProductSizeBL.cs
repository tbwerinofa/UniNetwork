using BusinessObject;
using BusinessObject.Component;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Interface
{
    public interface IProductSizeBL
    {

       Task<SaveResult> SaveProductSize(
             ProductViewModel parentViewModel);

        IEnumerable<SelectListItem> GetSelectListItems();

        IEnumerable<SelectListItem> GetSelectListItemByParentId(
          int productId);

        IQueryable<DataAccess.ProductSize> GetProductSizesByProductId(
           int productId);
    }
}
