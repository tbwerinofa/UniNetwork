using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public class ItemViewModel : BaseViewModel
    {

        [Required(ErrorMessage = "required")]
        [Range(1, 100, ErrorMessage = "required")]
        public int Quantity { get; set; }

        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required.")]
        [DisplayName("Product Category")]
        public int ProductCategoryId { get; set; }


        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required.")]
        [DisplayName("Product")]
        public int ProductId { get; set; }

        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required.")]
        [DisplayName("Product Size")]
        public int ProductSizeId{ get; set; }

        public IEnumerable<SelectListItem> ProductCategories { get; set; }

        public IEnumerable<SelectListItem> Products { get; set; }


        public IEnumerable<SelectListItem> ProductSizes { get; set; }

        public string ProductCategory { get; set; }

        public string Product { get; set; }

        public string Size { get; set; }


    }
}
