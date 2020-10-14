using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.ViewModel
{
    public class AwardTrophyViewModel : BaseViewModel
    {
        [Display(Name = "Trophy")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int TrophyId { get; set; }

        [Display(Name = "Start Year")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int FinYearId { get; set; }

        [Display(Name = "Award")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int AwardId { get; set; }

        public IEnumerable<SelectListItem> FinYears { get; set; }

        public IEnumerable<SelectListItem> Trophies { get; set; }

        public IEnumerable<SelectListItem> Awards { get; set; }

        public IEnumerable<SelectListItem> CalendarMonths { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        public int FinYear { get; set; }
        public string Trophy { get; set; }

        public string Award { get; set; }
        public string Gender { get; set; }
        public int Ordinal { get; set; }
    }
}
