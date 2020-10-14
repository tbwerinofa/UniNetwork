using BusinessObject.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public class ProductImageViewModel : BaseViewModel
    {

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Upload File")]
        [BindProperty]
        public IFormFile FileUploaded { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Order")]
        public int Ordinal { get; set; }

        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required.")]
        [Display(Name = "Product")]
        public int ProductId { get; set; }

        public string ProductCategory { get; set; }

        public string Product { get; set; }

        public decimal? Price { get; set; }

        //public IEnumerable<Size> Sizes { get; set; }


        public IEnumerable<FeaturedImageViewModel> FeaturedImages { get; set; }
        #region Drop Downs

        public IEnumerable<SelectListItem> Products { get; set; }

        [DisplayName("Featured Categories")]
        public IEnumerable<FeaturedCategoryViewModel> FeaturedCategories { get; set; }

        public IEnumerable<int> FeaturedCategoryIds { get; set; }

        #endregion

        [Display(Name = "Is Featured")]
        public bool IsFeatured { get; set; }

        public string DocumentName { get; set; }

        public string DocumentPath { get; set; }

        public string DocumentNameGuId { get; set; }

        public int DocumentId { get; set; }

        public DocumentViewModel Document { get; set; }

        public IEnumerable<SelectListItem> DocumentTypes { get; set; }

        public string DocumentExtension { get; set; }

        public string DocumentGuid { get; set; }



        public DateTime ProductCreatedTimestamp { get; set; }

        public int Hits { get; set; }
        public int Sold { get; set; }



    }
}
