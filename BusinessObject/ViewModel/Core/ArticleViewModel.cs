using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.ViewModel
{
    public class ArticleViewModel : BaseViewModel
    {

        [Required(ErrorMessage = "required")]
        [StringLength(100, ErrorMessage = "must be less than 100 characters")]
        public string Author { get; set; }

        [StringLength(100, ErrorMessage = "must be less than 100 characters")]
        [Display(Name = "Title")]
        public string Name { get; set; }

        // [AllowHtml]
        [Required(ErrorMessage = "required")]
        public string Body { get; set; }

        [Display(Name = "Publish Date")]
        public DateTime PublishDate { get; set; }

        [Display(Name = "Year")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int FinYearId { get; set; }

        public IEnumerable<SelectListItem> FinYears { get; set; }

        [Display(Name = "Issue No")]
        public int? IssueNo { get; set; }

        [Display(Name = "Month")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int CalendarMonthId { get; set; }

        public IEnumerable<SelectListItem> CalendarMonths { get; set; }

        public int FinYear { get; set; }
        public string CalendarMonth { get; set; }

        public int CalendarMonthOrdinal { get; set; }
    }
}
