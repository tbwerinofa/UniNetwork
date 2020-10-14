using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
	public class SubscriptionTypeViewModel : BaseViewModel
    {

		[Required(ErrorMessage = "required")]
		[StringLength(100, ErrorMessage = "must be less than 100 characters")]
		public string Name { get; set; }

        public decimal AmountRand { get; set; }

        public string AmountRandFormatted { get; set; }
        public int ActiveMonths { get; set; }

        public string Discriminator { get; set; }

        public bool HasSubscription { get; set; }

        public IEnumerable<SubscriptionTypeAttributeViewModel> SubscriptionTypeAttributeList { get; set; }

        public IEnumerable<SubscriptionTypeRuleViewModel> SubscriptionTypeRuleList { get; set; }

    }
}
