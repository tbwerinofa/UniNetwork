using BusinessObject.Component;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.ViewModel
{
    public class TradeBodyTypeViewModel : BaseViewModel
    {
        [Display(Name = "Name")]
        [StringLength(50, ErrorMessage = "must be less than 50 characters")]
        [Required(ErrorMessage = "required")]
        public string Name { get; set; }

        [Display(Name = "Key")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "must be 4 characters")]
        [Required(ErrorMessage = "required")]
        public string Discriminator { get; set; }
    }
}
