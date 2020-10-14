using System.ComponentModel.DataAnnotations;

namespace BusinessObject.ViewModel
{
    public class MeasurementUnitGroupViewModel : BaseViewModel
    {
        [Display(Name = "Name")]
        [StringLength(50, ErrorMessage = "must be less than 50 characters")]
        [Required(ErrorMessage = "required")]
        public string Name { get; set; }

    }
}
