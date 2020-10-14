using BusinessObject.Component;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.ViewModel
{
    public class OrganisationTypeViewModel : BaseViewModel
    {
        [Display(Name = "Name")]
        [StringLength(250, ErrorMessage = "must be less than 250 characters")]
        [Required(ErrorMessage = "required")]
        public string Name { get; set; }

    }
}
