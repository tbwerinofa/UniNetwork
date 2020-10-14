using BusinessObject.Component;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.ViewModel
{
    public class AgeGroupViewModel : BaseViewModel
    {
        [Display(Name = "Name")]
        [StringLength(250, ErrorMessage = "must be less than 250 characters")]
        [Required(ErrorMessage = "required")]
        public string Name { get; set; }

        public string MinValue { get; set; }

        public string MaxValue { get; set; }

    }
}
