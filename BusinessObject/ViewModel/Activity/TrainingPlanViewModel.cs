using BusinessObject.Component;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.ViewModel
{
    public class TrainingPlanViewModel : BaseViewModel
    {

        [StringLength(250, ErrorMessage = "must be less than 250 characters")]
        public string Name { get; set; }

        [Display(Name = "Objective")]
        [StringLength(500, ErrorMessage = "must be less than 500 characters")]
        public string Objective { get; set; }

        [Display(Name = "Year")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int FinYearId { get; set; }

        [Display(Name = "Training Plan")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int EventId { get; set; }

        [Display(Name = "Distance")]
        public int[] DistanceIds { get; set; }

        [Display(Name = "Members")]
        public int[] MemberIds { get; set; }

        [Display(Name = "Target Race")]
        public int[] RaceDefinitionIds { get; set; }

        public int FinYear { get; set; }

        public int DistanceCount { get; set; }
        public int MemberCount { get; set; }

        public IEnumerable<SelectListItem> Members { get; set; }
        public IEnumerable<SelectListItem> Distances { get; set; }
        public IEnumerable<SelectListItem> Events { get; set; }
        public IEnumerable<SelectListItem> FinYears { get; set; }

        public IEnumerable<SelectListItem> RaceDefinitions { get; set; }

        public string Event { get; set; }

    }


}
  