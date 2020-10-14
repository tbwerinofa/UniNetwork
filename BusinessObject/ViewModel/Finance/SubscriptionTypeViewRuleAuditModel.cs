using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public class SubscriptionTypeRuleAuditViewModel : BaseViewModel
    {

        public int SubscriptionTypeRuleId { get; set; }


		[Display(Name = "Amount (ZAR)")]
		[Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
		public decimal AmountRand { get; set; }


		[Display(Name = "Active Months")]
		public int? ActiveMonths { get; set; }

        [Display(Name = "Has Quantity")]
        public bool HasQuantity { get; set; }

        [Display(Name = "Includes Relatives")]
        public bool HasRelations { get; set; }

        public IEnumerable<SelectListItem> SubscriptionTypes { get; set; }

        public int Quantity { get; set; }

        [Display(Name = "Subscription Type")]
		[Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
		public int SubscriptionTypeId { get; set; }


		public string SubscriptionType { get; set; }


        public IEnumerable<SubscriptionTypeViewModel> SubscriptionTypeList { get; set; }

        public IEnumerable<SelectListItem> MemberMappings { get; set; }

        public string QuoteFullName { get; set; }

        public string QuoteUserId { get; set; }

        public string AgeGroup { get; set; }

        public bool IsMyAge { get; set; }
        public bool HasSubscription { get; set; }
    }
}
