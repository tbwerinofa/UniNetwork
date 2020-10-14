using BusinessObject.Component;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.ViewModel
{
    public class TimeTrialViewModel : BaseViewModel
    {


        public int FinYearId { get; set; }

        public int CalendarId { get; set; }


        public int CalendarMonthId { get; set; }


        public int RaceTypeId { get; set; }


        public int DistanceId { get; set; }

        public DateTime ScheduleDate { get; set; }

        public string ScheduleDateString { get; set; }

        public DateTime? RevisedDate { get; set; }

        public TimeSpan StartTime { get; set; }
        public int FinYear { get; set; }
        public string Discpline { get; set; }
        public string Venue { get; set; }
        public string Country { get; set; }
        public string RaceDefinition { get; set; }
        public CalendarViewModel Calendar { get; set; }
        public IEnumerable<SelectListItem> RaceTypes { get; set; }

        public IEnumerable<SelectListItem> CalendarMonths { get; set; }

        public IEnumerable<SelectListItem> Distances { get; set; }
        public IEnumerable<SelectListItem> FinYears { get; set; }

        public IEnumerable<int> TimeTrialDistanceViewModel { get; set; }

        public IEnumerable<TimeTrialDistanceViewModel> TimeTrialDistances { get; set; }

        public IEnumerable<SelectListItem> TimeTrialDistancesSelectList { get; set; }
        public IEnumerable<SelectListItem> Genders { get; set; }

        [Display(Name = "Distance")]
        [Required(ErrorMessage = "required")]
        public IEnumerable<int> DistanceIds { get; set; }

    }


}
  