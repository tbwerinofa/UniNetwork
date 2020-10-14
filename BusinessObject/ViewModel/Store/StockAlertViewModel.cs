using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public class StockAlertViewModel:BaseViewModel
    {

        public int ProductId { get; set; }

        public int? ProductSizeId { get; set; }

        public int? SizeId { get; set; }

        public string Product { get; set; }

        public string Size { get; set; }


        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "format is not valid")]
        [StringLength(50, ErrorMessage = "must be less than 50 characters")]
        [Required(ErrorMessage = "required")]
        public string Email { get; set; }

        public bool IsAlertSent { get; set; }


    }

}
