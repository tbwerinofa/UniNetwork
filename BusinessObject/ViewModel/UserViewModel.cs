using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.ViewModel
{
    public class UserViewModel: BaseViewModel
    {
        public new string Id { get; set; }
        public string UserName { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "required")]
        [StringLength(100, ErrorMessage = "must be less than 100 characters.")]
        public string FirstName { get; set; }

        [Display(Name = "Surname")]
        [Required(ErrorMessage = "required")]
        [StringLength(100, ErrorMessage = "must be less than 100 characters.")]
        public string Surname { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Contact Number")]
        [Required(ErrorMessage = "required")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "format is not valid.")]
        public string ContactNo { get; set; }

        //[Display(Name = "Password")]
        //[Required(ErrorMessage = "required")]
        //[StringLength(15, MinimumLength = 6, ErrorMessage = "must be between 6 and 15 characters")]
        //[DataType(DataType.Password)]
        public string Password { get; set; }


        //[Display(Name = "Confirm Password")]
        //[Required(ErrorMessage = "required")]
        //[StringLength(15, MinimumLength = 6, ErrorMessage = "must be between 6 and 15 characters")]
        //[System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "must match password")]
        //[DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "format is not valid")]
        [StringLength(50, ErrorMessage = "must be less than 50 characters")]
        [Required(ErrorMessage = "required")]
        public string Email { get; set; }

        [Display(Name = "Confirm Email")]
        [EmailAddress(ErrorMessage = "format is not valid")]
        [StringLength(50, ErrorMessage = "must be less than 50 characters")]
        [System.ComponentModel.DataAnnotations.Compare("Email", ErrorMessage = "must match email")]
        [Required(ErrorMessage = "required")]
        public string ConfirmEmail { get; set; }

        public bool EmailConfirmed { get; set; }

        public string FullName { get; set; }

        public string LastLoginShortDate { get; set; }

        public string Title { get; set; }

        public string Gender { get; set; }
    
        public DateTime CreatedTimestamp { get; set; }


        public DateTime LastLogin { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }

        [Display(Name = "Roles")]
        [Required(ErrorMessage = "required")]
        public IEnumerable<string> RoleIds { get; set; }
  

        public IEnumerable<SelectListItem> Regions { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }

        //public IEnumerable<DropDownListItems> CorporateUnits { get; set; }

        [Display(Name = "Restrict To Region")]
        public IEnumerable<int> RegionIds { get; set; }

        [Display(Name = "Country")]
        public int? CountryId { get; set; }

        [Display(Name = "Restrict to Plant")]
        public IEnumerable<int> CorporateUnitIds { get; set; }

        public int PersonId { get; set; }

    }
}
