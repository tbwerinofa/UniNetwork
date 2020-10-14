using BusinessObject.Component;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.ViewModel
{
    public class MyDashBoardViewModel : BaseViewModel
    {
        public MemberViewModel Member { get; set; }
        public IEnumerable<RaceResultViewModel> RaceResults { get; set; }

        public IEnumerable<TimeTrialResultViewModel> TimeTrialResults { get; set; }

    }


}
  