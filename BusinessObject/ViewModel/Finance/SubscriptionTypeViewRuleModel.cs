using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public class SubscriptionTypeRuleViewModel : BaseViewModel
    {


		[Display(Name = "Amount (ZAR)")]
		[Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
		public decimal AmountRand { get; set; }

		[Display(Name = "Active Months")]
		public int? ActiveMonths { get; set; }

	

		[Display(Name = "Subscription Type")]
		[Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
		public int SubscriptionTypeId { get; set; }


		public string SubscriptionType { get; set; }


        [Display(Name = "Age Group")]
        public int? AgeGroupId { get; set; }

        public IEnumerable<SubscriptionTypeViewModel> SubscriptionTypeList { get; set; }


        [Display(Name = "Subscription Type")]
        [Required(ErrorMessage = "required")]
        public bool AcceptQuote { get; set; }


        [Display(Name = "Has Quantity")]
        [Required(ErrorMessage = "required")]
        public bool HasQuantity { get; set; }
        public string AgeGroup { get; set; }

        public IEnumerable<SelectListItem> SubscriptionTypes { get; set; }

        public IEnumerable<SelectListItem> AgeGroups { get; set; }
        public bool HasRelations { get; set; }
    }
}
