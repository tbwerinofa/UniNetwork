using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public class OrderDetailStatusViewModel:BaseViewModel
    {

        [DisplayName("Name")]
        [Required(ErrorMessage = "required")]
        [MaxLength(50, ErrorMessage = "Order Status can not be more than 100 characters!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "required.")]
        public int Ordinal { get; set; }

        public ICollection<OrderDetailViewModel> OrderDetails { get; set; }

    }
}