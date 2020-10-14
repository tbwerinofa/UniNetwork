using BusinessObject.Component;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessObject.ViewModel
{
    public class EmploymentConditionRateViewModel: BaseViewModel
    {

        [DisplayName("Notes")]
        [StringLength(100, ErrorMessage = "must be less than 100 characters.")]
        public string Notes { get; set; }

        [DisplayName("Value")]
        public int PropertyValue { get; set; }

        [Display(Name = "Work Conditions")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int AgreementEmploymentConditionId { get; set; }

        [Display(Name = "Measurement Unit")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int MeasurementUnitId { get; set; }

        public int AgreementId { get; set; }

        public int EmploymentConditionId { get; set; }

        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "required")]
        public DateTime StartDate { get; set; }

        [DisplayName("End Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "required")]
        public DateTime EndDate { get; set; }

        public IEnumerable<SelectListItem> MeasurementUnits { get; set; }

        public IEnumerable<SelectListItem> AgreementEmploymentConditions { get; set; }

        public string EmploymentCondition { get; set; }

        public string MeasurementUnit { get; set; }

        public string StartDateLongDate { get; set; }

        public string EndDateLongDate { get; set; }


        public string AgreementType { get; set; }

        public string AgreementLevel { get; set; }
      
        public string EffectiveDateLongDate { get; set; }

        public string ExpiryDateLongDate { get; set; }
    }
}
