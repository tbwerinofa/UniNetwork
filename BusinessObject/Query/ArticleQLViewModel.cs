using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.ViewModel
{
    public class ArticleQLViewModel : BaseViewModel
    {

        public string Author { get; set; }


        public string Name { get; set; }

        // [AllowHtml]

        public string Body { get; set; }

        public int FinYearId { get; set; }

        public IEnumerable<SelectListItem> FinYears { get; set; }


        public int CalendarMonthId { get; set; }

        public IEnumerable<SelectListItem> CalendarMonths { get; set; }

        public int FinYear { get; set; }
        public string CalendarMonth { get; set; }

        public int IssueNo { get; set; }
    }
}
