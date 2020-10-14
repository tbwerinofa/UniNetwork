using BusinessObject.Component;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.ViewModel
{
    public class TimeTrialDistanceViewModel : BaseViewModel
    {
        public int TimeTrialId { get; set; }
        public int DistanceId { get; set; }
        public DateTime EventDateTimes { get; set; }

        public string TimeTrial { get; set; }

        public string Distance { get; set; }

        public int Participant { get; set; }

        public IEnumerable<TimeTrialResultViewModel> TimeTrialResults { get; set; }

    }
}
