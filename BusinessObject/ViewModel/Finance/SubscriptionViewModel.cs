using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace BusinessObject
{
    public class SubscriptionViewModel:BaseViewModel
	{

        public int QuoteDetailId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string QuoteNo { get; set; }

        public string SubscriptionType { get; set; }

        public IEnumerable<SelectListItem> SubscriptionTypes { get; set; }

        public string FullName { get; set; }

        public string MemberNo { get; set; }

        public SubscriptionTypeViewModel SubscriptionTypeModel { get; set;}

        public string SubscriptionUserId { get; set; }

        public string StartDateLongDate { get; set; }

        public string EndDateLongDate { get; set; }
        public bool HasExpired { get; set; }


    }
}
