using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessObject.ViewModel
{
    public class PersonViewModel: BaseViewModel
    {

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "required")]
        [StringLength(100, ErrorMessage = "must be less than 100 characters.")]
        public string FirstName { get; set; }

        [Display(Name = "Surname")]
        [Required(ErrorMessage = "required")]
        [StringLength(100, ErrorMessage = "must be less than 100 characters.")]
        public string Surname { get; set; }

        [Display(Name = "ID Number")]
        [Required(ErrorMessage = "required")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "must be equal to 13 characters")]
        public string IDNumber { get; set; }

        public string Initials { get; set; }


        [Display(Name = "Other name")]
        public string OtherName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Contact Number")]
        [Required(ErrorMessage = "required")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "format is not valid.")]
        public string ContactNo { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "required")]
        [EmailAddress(ErrorMessage = "format is not valid")]
        public string Email { get; set; }

        public IEnumerable<SelectListItem> Genders { get; set; }

        public IEnumerable<SelectListItem> Titles { get; set; }

        [Display(Name = "Gender")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int GenderId { get; set; }


        [Display(Name = "Nationality")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int CountryId { get; set; }



        [Display(Name = "ID Type")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int IDTypeId { get; set; }

        [Display(Name = "Title")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int TitleId { get; set; }

        public string Title { get; set; }

        public string Gender { get; set; }

        public string Country { get; set; }

        public string Region { get; set; }

        public string PersonGuid { get; set; }

        public string DocumentName { get; set; }

        public string DocumentPath { get; set; }

        public string DocumentNameGuId { get; set; }

        public int? DocumentId { get; set; }

    }
}
