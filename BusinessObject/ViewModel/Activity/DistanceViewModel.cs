using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.ViewModel
{
    public class DistanceViewModel : BaseViewModel
    {
        [Display(Name = "Name")]
        [StringLength(250, ErrorMessage = "must be less than 250 characters")]
        [Required(ErrorMessage = "required")]
        public string Name { get; set; }


        [StringLength(4, MinimumLength = 4, ErrorMessage = "must be four characters")]
        [Required(ErrorMessage = "required")]
        public string Discriminator { get; set; }

        [Display(Name = "Measurement")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int Measurement { get; set; }

        [Display(Name = "Unit")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int MeasurementUnitId { get; set; }

        public string MeasurementUnit { get; set; }

        public IEnumerable<SelectListItem> MeasurementUnits { get; set; }
    }
}
