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
    public class MemberPositionViewModel : BaseViewModel
    {

        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "required")]
        public DateTime StartDate { get; set; }

        public string StartDateLongDate { get; set; }

        public string PositionAudit { get; set; }

        public string MemberNo { get; set; }
        public int FinYearId { get; set; }

        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        [Display(Name = "Position")]
        public int PositionAuditId { get; set; }

        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        [Display(Name = "Member")]
        public int MemberId { get; set; }

        [Display(Name = "Operational Country")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int CountryId { get; set; }

        [Display(Name = "Region")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int RegionId { get; set; }


        [Display(Name = "Organisation Unit")]
        public int? CorporateUnitId { get; set; }

        public int? EmployeeId { get; set; }


        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        [Display(Name = "Structure")]
        public int OrganogramId { get; set; }

        public IEnumerable<DropDownListItems> CorporateUnits { get; set; }

        public IEnumerable<SelectListItem> Regions { get; set; }

        public IEnumerable<SelectListItem> FinYears { get; set; }

        public IEnumerable<SelectListItem> Members { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }

        public IEnumerable<SelectListItem> PositionAudits { get; set; }

        public IEnumerable<DropDownListItems> Organograms { get; set; }

        public MemberViewModel Member { get; set; }

        public int FinYear { get; set; }

        public string Country { get; set; }

        public string Region { get; set; }
        public string CorporateUnit { get; set; }

        public string CorporateCountry { get; set; }

        public string FullName { get; set; }
        public string EndDateLongDate { get; set; }

        public int MonthsInOffice { get; set; }

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

        public DateTime EmploymentDate { get; set; }
        public string EmploymentDateLongDate { get; set; }
    }
}
