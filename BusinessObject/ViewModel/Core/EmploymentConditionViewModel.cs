using BusinessObject.Component;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.ViewModel
{
    public class EmploymentConditionViewModel:BaseViewModel
    {
        [Display(Name = "Employment Conditions")]
        [StringLength(50, ErrorMessage = "must be less than 50 characters")]
        [Required(ErrorMessage = "required")]
        public string Name { get; set; }

    }
}
