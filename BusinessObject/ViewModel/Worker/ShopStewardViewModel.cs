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
    public class ShopStewardViewModel : BaseViewModel
    {

        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "required")]
        public DateTime StartDate { get; set; }

        public string StartDateLongDate { get; set; }

        [DisplayName("MemberNo")]
        [Required(ErrorMessage = "required")]
        [StringLength(50, ErrorMessage = "must be less than 50 characters.")]
        public string MemberNo { get; set; }
        public int FinYearId { get; set; }

        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        [Display(Name = "Term (years)")]
        public int ShopStewardSettingAuditId { get; set; }

        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        [Display(Name = "Trade")]
        public int TradeId { get; set; }

        [Display(Name = "Operational Country")]
        public int CountryId { get; set; }

        [Display(Name = "Region")]
        public int RegionId { get; set; }


        [Display(Name = "Plant")]
        public int MNCPlantId { get; set; }

        [Display(Name = "Company")]
        public int MNCCountryId { get; set; }


        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        [Display(Name = "Shop Steward Position")]
        public int OrganogramId { get; set; }

        public IEnumerable<DropDownListItems> MNCPlants { get; set; }

        public IEnumerable<DropDownListItems> Regions { get; set; }

        public IEnumerable<SelectListItem> FinYears { get; set; }

        public IEnumerable<SelectListItem> Trades { get; set; }

        public IEnumerable<SelectListItem> EmploymentStatuses { get; set; }

        public IEnumerable<SelectListItem> ShopStewardSettingAudits { get; set; }

        public IEnumerable<DropDownListItems> Organograms { get; set; }

        public EmployeeViewModel Employee { get; set; }

        public MemberViewModel Member { get; set; }

        public int FinYear { get; set; }

        public string Country { get; set; }

        public string Region { get; set; }
        public string MNCPlant { get; set; }

        public string MNCCountry { get; set; }

        public string FullName { get; set; }
        public string EndDateLongDate { get; set; }

        public int CourseCount { get; set; }

        public IEnumerable<RaceResultViewModel> Participants { get; set; }
        public IEnumerable<CourseHistoryViewModel> Courses { get; set; }

        public int PersonId { get; set; }
        public string EmployeeNo { get; set; }

        [DisplayName("Structure Level")]
        public string StructureLevel { get; set; }

        [DisplayName("Structure Type")]
        public string StructureType { get; set; }

        public string Structure { get; set; }


        [DisplayName("Employment Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "required")]
        public DateTime EmploymentDate { get; set; }
        public string EmploymentDateLongDate { get; set; }
    }
}
