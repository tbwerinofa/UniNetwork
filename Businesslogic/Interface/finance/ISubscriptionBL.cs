using BusinessObject;
using BusinessObject.Component;
using BusinessObject.ViewModel;
using System.Collections.Generic;

namespace BusinessLogic.Interface
{
    public interface ISubscriptionBL
    {
        IEnumerable<SubscriptionTypeRuleAuditViewModel> GetEntityByType_UserId(
      string userId,
    int? subscriptionTypeId,
    int? personId);

        void GenerateList(
     QuoteViewModel model,
     DataAccess.Quote entity);
    }
}
