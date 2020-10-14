using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public class SortCategoryViewModel : BaseViewModel
    {

        [DisplayName("Category Name")]
        [Required(ErrorMessage = "required")]
        [MaxLength(50, ErrorMessage = "Category name can not be more than 50 characters!")]
        public string Name { get; set; }

        [StringLength(4, MinimumLength = 4, ErrorMessage = "must be four characters")]
        [Required(ErrorMessage = "required")]
        public string Discriminator { get; set; }

        [Required(ErrorMessage = "required.")]
        public int Ordinal { get; set; }


    }
}