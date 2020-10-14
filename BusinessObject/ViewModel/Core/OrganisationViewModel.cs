using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessObject.ViewModel
{
    public class OrganisationViewModel : BaseViewModel
    {

        [Display(Name = "Name")]
        [StringLength(200, ErrorMessage = "must be less than 200 characters.")]
        [Required(ErrorMessage = "required")]
        public string Name { get; set; }

        [Display(Name = "Abbreviation")]
        [StringLength(20, ErrorMessage = "must be less than 20 characters.")]
        [Required(ErrorMessage = "required")]
        public string Abbreviation { get; set; }

        [Display(Name = "Organisation Type")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int OrganisationTypeId { get; set; }

        [Display(Name = "Province")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int ProvinceId { get; set; }

        [Display(Name = "Country")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int CountryId { get; set; }

        public string Province { get; set; }

        public string Country { get; set; }

        public string OrganisationType { get; set; }

        public IEnumerable<SelectListItem> Provinces { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }
        public IEnumerable<SelectListItem> OrganisationTypes { get; set; }
    }
}

