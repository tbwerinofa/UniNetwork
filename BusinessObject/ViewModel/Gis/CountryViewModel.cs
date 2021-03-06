﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.ViewModel
{
    public class CountryViewModel : BaseViewModel
    {
        [Display(Name = "Name")]
        [StringLength(100, ErrorMessage = "must be less than 100 characters")]
        [Required(ErrorMessage = "required")]
        public string Name { get; set; }

        public string GlobalRegion { get; set; }
 
        [Display(Name = "Global Region")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int GlobalRegionId { get; set; }


        public IEnumerable<SelectListItem> GlobalRegions { get; set; }

    }
}
