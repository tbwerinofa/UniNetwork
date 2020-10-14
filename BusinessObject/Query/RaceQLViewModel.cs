using BusinessObject.Component;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.ViewModel
{
    public class RaceQLViewModel : BaseViewModel
    {
        [Display(Name = "Theme")]
        public string Theme { get; set; }


        [Display(Name = "Year")]
        public int FinYearId { get; set; }

        [Display(Name = "Country")]
        public int CountryId { get; set; }

        [Display(Name = "Province")]
        public int ProvinceId { get; set; }

        [Display(Name = "Race Definition")]
        public int RaceDefinitionId { get; set; }

        [Display(Name = "Organiser Type")]
        public int OrganisationTypeId { get; set; }

        [Display(Name = "Organiser")]
        public int OrganisationId { get; set; }

        public IEnumerable<int> OrganisationIds { get; set; }

        public int FinYear { get; set; }
        public string Discpline { get; set; }
        public string RaceType { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string RaceDefinition { get; set; }
        public IEnumerable<SelectListItem> Provinces { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }
        public IEnumerable<SelectListItem> Distances { get; set; }
        public IEnumerable<SelectListItem> RaceDefinitions { get; set; }
        public IEnumerable<SelectListItem> FinYears { get; set; }
        public IEnumerable<SelectListItem> OrganisationTypes { get; set; }
        public IEnumerable<SelectListItem> Organisations { get; set; }

        public int Participants { get; set; }
        public int?[] DistanceIds { get; set; }
        public DateTime EventDateTimes { get; set; }

        public IEnumerable<int> RaceDistanceViewModel { get; set; }

        public IEnumerable<RaceDistanceViewModel> RaceDistances { get; set; }

        public IEnumerable<SelectListItem> RaceDistancesSelectList { get; set; }
        public IEnumerable<SelectListItem> Genders { get; set; }

        public int SessionPersonId { get; set; }

    }


}
  