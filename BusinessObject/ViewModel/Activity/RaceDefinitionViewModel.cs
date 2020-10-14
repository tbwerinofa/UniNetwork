using BusinessObject.Component;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.ViewModel
{
    public class RaceDefinitionViewModel : BaseViewModel
    {
        [Display(Name = "Name")]
        [StringLength(250, ErrorMessage = "must be less than 250 characters")]
        [Required(ErrorMessage = "required")]
        public string Name { get; set; }

        [Display(Name = "Country")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int CountryId { get; set; }

        [Display(Name = "Province")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int ProvinceId { get; set; }

        [Display(Name = "Discpline")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int DiscplineId { get; set; }

        [Display(Name = "Race Type")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int RaceTypeId { get; set; }

        public string RaceType { get; set; }
        public string Discpline { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }

        public IEnumerable<SelectListItem> Provinces { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }
        public IEnumerable<SelectListItem> Discplines { get; set; }
        public IEnumerable<SelectListItem> RaceTypes { get; set; }

        public IEnumerable<RaceQLViewModel> Races { get; set; }
    }
}
