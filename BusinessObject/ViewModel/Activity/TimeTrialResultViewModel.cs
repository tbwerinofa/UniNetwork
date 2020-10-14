using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.ViewModel
{
    public class TimeTrialResultViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "required")]
        public IEnumerable<int> MemberIds { get; set; }

        [Display(Name = "Distance")]
        public int TimeTrialDistanceId { get; set; }

        public int TimeTrialId { get; set; }

        public IEnumerable<SelectListItem> Genders { get; set; }


        [Display(Name = "Distance")]
        public int DistanceId { get; set; }

        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        [Display(Name = "Gender")]
        public int GenderId { get; set; }

        [Display(Name = "Member")]
        public int MemberId { get; set; }

        public int? Position { get; set; }

        public string MemberNo { get; set; }
        public string FullName { get; set; }
        public string AgeGroup { get; set; }
        public string Gender { get; set; }

        [Required(ErrorMessage = "required")]
        public TimeSpan TimeTaken { get; set; }

        public TimeSpan AveragePace { get; set; }

        public TimeTrialViewModel TimeTrial { get; set; }

        public string Distance { get; set; }
        public int Measurement { get; set; }
        public string RaceDefinition { get; set; }
        public DateTime EventDate { get; set; }
        public int PersonId { get; set; }

        public int Year { get; set; }
    }
}
