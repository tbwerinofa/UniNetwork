using BusinessObject.Component;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.ViewModel
{
    public class CommonViewModel : BaseViewModel
    {
        [Display(Name = "Name")]
        [StringLength(100, ErrorMessage = "must be less than 100 characters")]
        [Required(ErrorMessage = "required")]
        public string Name { get; set; }

        [StringLength(100, ErrorMessage = "must be less than 100 characters")]
        public string Description { get; set; }
    }
}
