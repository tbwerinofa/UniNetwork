using BusinessObject.Component;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.ViewModel
{
    public class RemittanceViewModel : BaseViewModel
    {

        public int FinYear { get; set; }

        public string CalendarMonth { get; set; }

        public string MNCPlant { get; set; }

        public string FullName { get; set; }

        public string MemberNo { get; set; }

        public string EmployeeNo { get; set; }

        public string Trade { get; set; }

        public string EmploymentDateString { get; set; }

        public int EmployeeId { get; set; }

        public int RemittanceSummaryId { get; set; }

        public bool IsVerified { get; set; }

        public decimal Amount { get; set; }

        public string IDNumber { get; set; }

        public string DocumentName { get; set; }

        public string CurrencyAmount { get; set; }

        public IEnumerable<int> SelectedIdList { get; set; }

        public bool HasNotVerified { get; set; }

    }
}
