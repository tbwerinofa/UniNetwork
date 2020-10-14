using BusinessObject.Component;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.ViewModel
{
    public class CapacityBuildingViewModel : BaseViewModel
    {

        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "required")]
        public DateTime StartDate { get; set; }

        [DisplayName("End Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "required")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Year")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int FinYearId { get; set; }

        [Display(Name = "Course")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int CourseId { get; set; }

        public IEnumerable<SelectListItem> Courses { get; set; }

        public IEnumerable<SelectListItem> FinYears { get; set; }

        public string Course { get; set; }
     
        public int FinYear { get; set; }

        public string StartDateLongDate { get; set; }

        public string EndDateLongDate { get; set; }

        public IEnumerable<RaceResultViewModel> Participants { get; set; }

        public int ParticipantCount { get; set; }

        public IEnumerable<CourseHistoryViewModel> CourseHistory { get; set; }
    }
}
