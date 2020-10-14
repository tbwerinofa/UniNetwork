using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public class SubscriptionTypeAttributeViewModel : BaseViewModel
    {

        public string SubscriptionType { get; set; }

        [StringLength(50, ErrorMessage = "must be less than 50 characters")]
        public string Name { get; set; }

        public IEnumerable<SelectListItem> SubscriptionTypes { get; set; }


        [Display(Name = "Subscription Type")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int SubscriptionTypeId { get; set; }
    }
}
