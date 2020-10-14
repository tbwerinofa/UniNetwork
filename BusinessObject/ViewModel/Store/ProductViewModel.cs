using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public class ProductViewModel : BaseViewModel
    {

        [Required(ErrorMessage = "required")]
        [StringLength(50, ErrorMessage = "must be less than 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "required")]
        [StringLength(250, ErrorMessage = "must be less than 250 characters")]
        public string Description { get; set; }

        [DisplayName("Category")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required.")]
        public int ProductCategoryId { get; set; }

        [DisplayName("Size")]
        //[Range(1, 100, ErrorMessage = "required.")]
        public int ProductSizeId { get; set; }

        [DisplayName("Size")]
        public int? SizeId { get; set; }

        [Required(ErrorMessage = "required")]
        [DisplayName("Sort Order")]
        public int Ordinal { get; set; }

        [Required(ErrorMessage = "required")]
        public decimal Price { get; set; }

        public string ProductCategory { get; set; }


        [DisplayName("Is Main")]
       // [Required(ErrorMessage = "required")]
        public bool IsMain { get; set; }


        [Required(ErrorMessage = "required")]
        [Range(1, 100, ErrorMessage = "at least 1 is required.")]
        public int Quantity { get; set; }


        public IEnumerable<ProductViewModel> Products { get; set; }

        public IEnumerable<BannerImageViewModel> BannerImages { get; set; }


        #region Drop Downs

        public IEnumerable<SelectListItem> ProductCategories { get; set; }

        #endregion

        public IEnumerable<ProductImageViewModel> ProductImages { get; set; }


        public IEnumerable<SelectListItem> ProductSizes { get; set; }

        public IEnumerable<SelectListItem> Items { get; set; }

        public IEnumerable<ItemViewModel> StockItems { get; set; }

        public IEnumerable<SizeViewModel> Sizes { get; set; }

        public IEnumerable<int> SizesId { get; set; }

        public IEnumerable<SelectListItem> SleeveTypes { get; set; }

    }
}
