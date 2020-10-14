using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public class SizeViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "required")]
        [MaxLength(4, ErrorMessage = "cannot exceed 4 characters!")]
        [DisplayName("Short Name")]
        public string ShortName { get; set; }

        [Required(ErrorMessage = "required")]
        [MaxLength(50, ErrorMessage = "cannot exceed 50 characters!")]
        [DisplayName("Size")]
        public string Name { get; set; }

        [Required(ErrorMessage = "required")]
        [DisplayName("Sort Order")]
        public int Ordinal { get; set; }

    }
}