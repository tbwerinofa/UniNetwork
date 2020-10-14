using BusinessObject.Component;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.ViewModel
{
    public class TradeBodyViewModel : BaseViewModel
    {
        [Display(Name = "Name")]
        [StringLength(250, ErrorMessage = "must be less than 250 characters")]
        [Required(ErrorMessage = "required")]
        public string Name { get; set; }

        [Display(Name = "Abbreviation")]
        [StringLength(15, ErrorMessage = "must be less than 15 characters")]
        [Required(ErrorMessage = "required")]
        public string Abbreviation { get; set; }

        [Display(Name = "Is Statutory Body")]
        public bool IsStatutory { get; set; }

        [Display(Name = "Type")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int TradeBodyTypeId { get; set; }


        [Display(Name = "Country")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int CountryId { get; set; }

        public string Country { get; set; }  

        public string TradeBodyType { get; set; }

        public IEnumerable<SelectListItem> TradeBodyTypes { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }
    }
}
