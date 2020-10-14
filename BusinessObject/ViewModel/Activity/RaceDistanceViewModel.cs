using BusinessObject.Component;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.ViewModel
{
    public class RaceDistanceViewModel : BaseViewModel
    {
        public int RaceId { get; set; }
        public int DistanceId { get; set; }
        public DateTime EventDateTimes { get; set; }

        public string Race { get; set; }

        public string Distance { get; set; }

        public int Participant { get; set; }

        public IEnumerable<RaceResultViewModel> RaceResults { get; set; }

    }
}
