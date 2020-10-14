namespace BusinessObject
{
    public class QuoteDetailViewModel : BaseViewModel
    {
        //public int Id { get; set; }

        public int QuoteId { get; set; }

        public string SubscriptionType { get; set; }

        public int SubscriptionTypeRuleAuditId { get; set; }

        public int Quantity { get; set; }

        public string UnitPriceCurrency { get; set; }

        public string AmountCurrency { get; set; }
    }
}
