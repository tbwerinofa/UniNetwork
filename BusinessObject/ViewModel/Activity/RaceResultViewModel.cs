using BusinessObject.Component;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.ViewModel
{
    public class RaceResultViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "required")]
        public IEnumerable<int> MemberIds { get; set; }

        public int RaceId { get; set; }

        public IEnumerable<SelectListItem> Genders { get; set; }


        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        [Display(Name = "Distance")]
        public int RaceDistanceId { get; set; }

        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        [Display(Name = "Gender")]
        public int GenderId { get; set; }

        [Display(Name = "Member")]
        public int MemberId { get; set; }

        public int PersonId { get; set; }

        public int? Position { get; set; }

        public string MemberNo { get; set; }
        public string FullName { get; set; }
        public string AgeGroup { get; set; }
        public string Gender { get; set; }

        [Required(ErrorMessage = "required")]
        public TimeSpan TimeTaken { get; set; }

        public TimeSpan AveragePace { get; set; }

        public RaceViewModel Race { get; set; }

        public string Distance { get; set; }
        public int Measurement { get; set; }
        public string RaceDefinition { get; set; }
        public DateTime EventDate { get; set; }
        public int FinYear { get; set; }

        public string DocumentName { get; set; }

        public string DocumentPath { get; set; }

        public string DocumentNameGuId { get; set; }

        public int? DocumentId { get; set; }
    }
}
