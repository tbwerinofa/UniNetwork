using BusinessObject;
using BusinessObject.Component;
using BusinessObject.ViewModel;
using System.Collections.Generic;

namespace BusinessLogic.Interface
{
    public interface ISubscriptionTypeBL
    {
        IEnumerable<SubscriptionTypeViewModel> GetEntityList(
              string userId);
    }
}
