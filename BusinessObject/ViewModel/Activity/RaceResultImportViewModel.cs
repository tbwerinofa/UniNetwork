using BusinessObject.Component;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.ViewModel
{
    public class RaceResultImportViewModel : BaseViewModel
    {

        public IEnumerable<SelectListItem> People { get; set; }

   
        public string RaceDefinition { get; set; }

        public string RaceType { get; set; }
        public string Discpline { get; set; }
        public string Province { get; set; }
        public int FinYear { get; set; }
        public DateTime EventDate { get; set; }
        public string Distance { get; set; }
        public string AgeGroup { get; set; }
        public string Organisation { get; set; }
        public string FirstName { get; set; }

        public string Surname { get; set; }
        public int Discriminator { get; set; }
        public string FullName { get; set; }
        public int Position { get; set; }
        public TimeSpan TimeTaken { get; set; }
        public int? RaceDistanceId { get; set; }

        public int[] SelectedIdArr { get; set; }

        public int[] PersonIdArr { get; set; }

        public IEnumerable<RaceResultImportDetailViewModel> RaceResultImportDetailViewModel { get; set; }
    }
}
