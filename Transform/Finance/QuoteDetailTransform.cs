using BusinessObject;
using DomainObjects;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class QuoteDetailTransform
    {
        /// <summary>
        /// Convert Quote Object into Quote Entity
        /// </summary>
        ///<param name="entity">IQueryable DataAccess.Quote</param>
        ///<returns>IEnumerable Quote</returns>
        ///
        public static IEnumerable<QuoteDetailViewModel> ToListViewModel(
            this DataAccess.Quote entity)
        {

            return entity.QuoteDetails.ToList().Select(a =>
                        new QuoteDetailViewModel
                        {
                            Id = a.Id,
                            QuoteId = a.QuoteId,
                            SubscriptionType= a.SubscriptionTypeRuleAudit.SubscriptionTypeRule.SubscriptionType.Name,
                            SubscriptionTypeRuleAuditId = a.SubscriptionTypeRuleAuditId,
                            Quantity = a.Quantity,
                            UnitPriceCurrency = a.SubscriptionTypeRuleAudit.AmountRand.ToMonetaryValue(),
                            AmountCurrency = (a.Quantity * a.SubscriptionTypeRuleAudit.AmountRand).ToMonetaryValue()



        }).AsEnumerable();
        }

    }
}
