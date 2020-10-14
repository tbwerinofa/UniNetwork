using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BusinessObject
{
    public class OnlineStoreViewModel
    {
        public IEnumerable<SelectListItem> SortCategories { get; set; }

        public IEnumerable<ProductCategoryViewModel> ProductCategories { get; set; }

        public int SortCategoryId { get; set; }

        public string DocumentPath { get; set; }
    }
}
