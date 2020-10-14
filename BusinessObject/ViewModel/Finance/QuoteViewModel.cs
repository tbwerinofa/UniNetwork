using BusinessObject.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public class QuoteViewModel : BaseViewModel
    {

        [DisplayName("Quote Reference")]
        public string QuoteNo { get; set; }

        [DisplayName("Quote Detail")]
        public string Details { get; set; }

        [DisplayName("Bank Reference")]
        [Required(ErrorMessage = "required")]
        [StringLength(250, ErrorMessage = "must be less than 250 characters.")]
        public string PaymentReference { get; set; }

        [DisplayName("Payment Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "required")]
        public DateTime? PaymentDate { get; set; }

        [Display(Name = "Quote Status")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int QuoteStatusId { get; set; }

        public string UserId { get; set; }

        public int[] SubscriptionTypeRuleAuditIds { get; set; }

        public int[] OrganisationIds { get; set; }

        public int[] QuantityIds { get; set; }

        public bool[] HasOrganisationIds { get; set; }

        public string CreatedTimestamp { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public decimal Amount { get; set; }
        public string QuoteStatus { get; set; }

        public string QuoteStatusRef { get; set; }

        public string SubscriptionType { get; set; }

        public int SubscriptionCount { get; set; }

        public IEnumerable<QuoteStatusViewModel> QuoteStatuses { get; set; }

        public IEnumerable<QuoteDetailViewModel> QuoteDetails { get; set; }

        public bool RequiresPayment { get; set; }

        public string FullName { get; set; }

        public string UserCode { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal TotalAmountExVat { get; set; }

        public decimal TotalVatAmount { get; set; }

        public int FinYearId { get; set; }

        public int FinYear { get; set; }

        public string TotalAmountRand { get; set; }

        public string TotalAmountExVatRand { get; set; }

        public string TotalVatAmountRand { get; set; }

        public PayFastViewModel PayFast { get; set; }

        public MessageTemplateViewModel MessageTemplate { get; set; }

        public string Email { get; set; }
        public string LastUpdateBy { get; set; }

        public IEnumerable<int> MemberListIds { get; set; }

        public int ParentMemberId { get; set; }
    }
}
