using BusinessObject.Component;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.ViewModel
{
    public class SubscriptionDashBoardViewModel : BaseViewModel
    {
        public IEnumerable<SubscriptionViewModel> Subscriptions { get; set; }

        public IEnumerable<QuoteViewModel> Quotes { get; set; }

        public IEnumerable<DashboardItem> SubscriptionYTD { get; set; }

    }


}
  