using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessObject.ViewModel
{
    public class WinnerViewModel: BaseViewModel
    {

        [Display(Name = "Year")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int FinYearId { get; set; }

        [Display(Name = "Month")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int CalendarMonthId { get; set; }


        [Display(Name = "Member")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int MemberId { get; set; }

        [Display(Name = "Award")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int AwardId { get; set; }

        [Display(Name = "Reccurence")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int FrequencyId { get; set; }

        public IEnumerable<SelectListItem> Awards { get; set; }

        public IEnumerable<SelectListItem> Frequencies { get; set; }

        public IEnumerable<SelectListItem> FinYears { get; set; }
        public IEnumerable<SelectListItem> Members { get; set; }
        public IEnumerable<SelectListItem> CalendarMonths { get; set; }

        public string Award { get; set; }

        public int FinYear { get; set; }
        public string Trophy { get; set; }
        public string CalendarMonth { get; set; }
        public string Suburb { get; set; }

        public string Member { get; set; }

        public int Ordinal { get; set; }
        public string Gender { get; set; }

        public string Frequency { get; set; }


    }
}
