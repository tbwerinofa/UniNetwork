using BusinessObject.Component;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.ViewModel
{
    public class CalendarViewModel : BaseViewModel
    {

        [DisplayName("Schedule Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "required")]
        public DateTime ScheduleDate { get; set; }

        [DisplayName("Revised Date")]
        [DataType(DataType.Date)]
        public DateTime? RevisedDate { get; set; }

        [DisplayName("Start Time")]
        [DataType(DataType.Time)]
        [Required(ErrorMessage = "required")]
        public TimeSpan StartTime { get; set; }

        public int FinYear { get; set; }

        public string Event { get; set; }

        public string Venue { get; set; }
        public string Notes { get; set; }


        [Display(Name = "Event")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int EventId { get; set; }


        [Display(Name = "Venue")]
        public int VenueId { get; set; }

        [Display(Name = "Year")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int FinYearId { get; set; }

        [Display(Name = "Moderator (Host)")]
        public IEnumerable<int> ModeratorIds { get; set; }


        public IEnumerable<SelectListItem> FinYears { get; set; }

        public IEnumerable<SelectListItem> Venues { get; set; }

        public IEnumerable<SelectListItem> Events { get; set; }

        public IEnumerable<DropDownListItems> Moderators { get; set; }

        public string ScheduleDateString { get; set; }
        public string RevisedDateString { get; set; }

        [Display(Name = "Month")]
        public int CalendarMonthId { get; set; }

        public IEnumerable<SelectListItem> CalendarMonths { get; set; }

        public IEnumerable<string> Members { get; set; }
    }
}

