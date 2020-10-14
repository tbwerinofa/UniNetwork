using BusinessObject.Component;
using BusinessObject.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessObject
{
    public class IncidentViewModel : BaseViewModel
    {

        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "required")]
        public DateTime IncidentDate { get; set; }

        public string IncidentDateLongDate { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "required")]
        [StringLength(500, ErrorMessage = "must be less than 50 characters.")]
        public string Description { get; set; }

        [Required, Range(0, Int32.MaxValue, ErrorMessage = "required")]
        [Display(Name = "Casualties")]
        public int CasualtyCount { get; set; }

        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        [Display(Name = "FinYear")]
        public int FinYearId { get; set; }

        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        [Display(Name = "Incident Group")]
        public int IncidentGroupId { get; set; }

        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        [Display(Name = "Incident Type")]
        public int IncidentTypeId { get; set; }

        [Display(Name = "Corporate")]
        [Required(ErrorMessage = "required")]
        public IEnumerable<int> CorporateCountryIds { get; set; }

        [Display(Name = "Plant")]
        public IEnumerable<int> CorporateUnitIds { get; set; }

        public IEnumerable<DropDownListItems> CorporateUnits { get; set; }

        public IEnumerable<SelectListItem> FinYears { get; set; }

        public IEnumerable<DropDownListItems> CorporateCountries { get; set; }

        public IEnumerable<SelectListItem> IncidentGroups { get; set; }

        public IEnumerable<SelectListItem> IncidentTypes { get; set; }

        public int FinYear { get; set; }

        public string IncidentType { get; set; }


        public string IncidentGroup { get; set; }

        public int IncidentCorporateCountryId { get; set; }


    }
}
