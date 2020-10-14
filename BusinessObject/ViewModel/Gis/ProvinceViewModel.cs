using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.ViewModel
{
    public class ProvinceViewModel : BaseViewModel
    {
        [Display(Name = "Name")]
        [StringLength(50, ErrorMessage = "must be less than 50 characters")]
        [Required(ErrorMessage = "required")]
        public string Name { get; set; }

        public string Country { get; set; }
 
        [Display(Name = "Country")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int CountryId { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }

    }
}
