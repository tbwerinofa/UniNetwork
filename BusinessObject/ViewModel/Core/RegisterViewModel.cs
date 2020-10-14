using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace BusinessObject.ViewModel
{
    public class RegisterViewModel:BaseViewModel
    {

        [EmailAddress(ErrorMessage = "format is not valid")]
        [StringLength(50, ErrorMessage = "must be less than 50 characters")]
        [Required(ErrorMessage = "required")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string PersonGuid { get; set; }

        [Display(Name = "Fammilly Members at club")]
        public IEnumerable<int> MemberIds { get; set; }

        [Display(Name = "Profile Picture")]
        public int ImageId { get; set; }

        public IEnumerable<SelectListItem> Members { get; set; }
        public bool HasAccount { get; set; }
    }

}
