using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public class OrderDetailViewModel : BaseViewModel
    {

        [Required(ErrorMessage = "required.")]
        public int OrderDetailId { get; set; }

        [Required(ErrorMessage = "required.")]
        public int QuoteId { get; set; }

        [DisplayName("Product")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required.")]
        public int ProductId { get; set; }

        [DisplayName("Size")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required.")]
        public int SizeId { get; set; }

        [Required(ErrorMessage = "required.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "required.")]
        public decimal UnitPrice { get; set; }

        public int ProductSizeId { get; set; }

        [DisplayName("Product Category")]
        public int ProductCategoryId { get; set; }

        public ProductViewModel Product { get; set; }

        public virtual SizeViewModel Size { get; set; }
        public virtual QuoteViewModel Quote { get; set; }

        public IEnumerable<SelectListItem> Products { get; set; }

        public IEnumerable<SelectListItem> ProductCategories { get; set; }

        public IEnumerable<SelectListItem> Sizes { get; set; }

    }
}