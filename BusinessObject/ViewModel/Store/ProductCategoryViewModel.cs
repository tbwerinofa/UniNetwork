using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public class ProductCategoryViewModel : BaseViewModel
    {

        [DisplayName("Category Name")]
        [Required(ErrorMessage = "required")]
        [MaxLength(50, ErrorMessage = "Category name can not be more than 50 characters!")]
        public string Name { get; set; }


        public string Description { get; set; }

        [Required(ErrorMessage = "required.")]
        public int Ordinal { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }

        public IEnumerable<BannerImageViewModel> BannerImages { get; set; }

    }
}