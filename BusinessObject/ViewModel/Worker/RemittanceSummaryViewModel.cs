using BusinessObject.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public class RemittanceSummaryViewModel : BaseViewModel
    {

        public int FinYear { get; set; }

        public string MNCPlant { get; set; }
        public string MNCCountry { get; set; }

        public string CalendarMonth { get; set; }

        [Display(Name = "Plant")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int MNCPlantId { get; set; }

        [Display(Name = "Corporate")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int MNCCountryId { get; set; }

        [Display(Name = "Year")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int FinYearId { get; set; }

        [Display(Name = "Month")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int CalendarMonthId { get; set; }
        public int StateMachineId { get; set; }

        public string CurrencyAmount { get; set; }

        public int RecordCount { get; set; }

        public DocumentViewModel Document { get; set; }

        public IEnumerable<SelectListItem> FinYears { get; set; }

        public IEnumerable<SelectListItem> CalendarMonths { get; set; }

        public IEnumerable<SelectListItem> MNCCountries { get; set; }

        public IEnumerable<SelectListItem> MNCPlants { get; set; }

        public IEnumerable<SelectListItem> DocumentTypes { get; set; }

        public string DocumentName { get; set; }

        public string DocumentExtension { get; set; }

        public string DocumentPath { get; set; }

        public string DocumentGuid { get; set; }

        public int DocumentId { get; set; }

        public string Abbreviation { get; set; }

        public string WorkFlowStatus { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Upload File")]
        public IFormFile FileUploaded { get; set; }

        public bool HasNotVerified { get; set; }

        public bool IsProcessed { get; set; }

        public bool IsFailed { get; set; }

        public bool IsRemittanceError { get; set; }
    }
}

