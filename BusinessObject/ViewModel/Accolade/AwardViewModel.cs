using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.ViewModel
{
    public class AwardViewModel : BaseViewModel
    {
        [Display(Name = "Name")]
        [StringLength(100, ErrorMessage = "must be less than 100 characters")]
        [Required(ErrorMessage = "required")]
        public string Name { get; set; }

        [Display(Name = "Recurrence")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int FrequencyId { get; set; }

        [Display(Name = "Gender")]
        public int? GenderId { get; set; }

        public IEnumerable<SelectListItem> Frequencies { get; set; }

        public IEnumerable<SelectListItem> Genders { get; set; }

        [Display(Name = "Has Trophy")]
        public bool HasTrophy { get; set; }

        public string Frequency { get; set; }
        public string Gender { get; set; }

        [Display(Name = "Position")]
        public int Ordinal { get; set; }

    }
}
