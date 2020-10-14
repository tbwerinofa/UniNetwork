using BusinessObject.Component;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.ViewModel
{
    public class TrainingPlanQLViewModel : BaseViewModel
    {


        public string Name { get; set; }

        [Display(Name = "Objective")]
        public string Objective { get; set; }

        [Display(Name = "Year")]

        public int FinYearId { get; set; }

        [Display(Name = "Training Plan")]

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

        public string EventName { get; set; }

        public IEnumerable<MemberListViewModel> Members { get; set; }
        public EventViewModel Event { get; set; }
        public IEnumerable<string> Distances { get; set; }
        public IEnumerable<string> RaceDefinitions { get; set; }

    }


}
  