using BusinessObject;
using BusinessObject.Component;
using BusinessObject.ViewModel;
using System.Threading.Tasks;

namespace BusinessLogic.Interface
{
    public interface IQuoteBL
    {
        Task<PayFastViewModel> SavePayFast(QuoteViewModel model);

        Task<SaveResult> UpdateQuoteStatus(
            QuoteViewModel model);

        Task<QuoteViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null);

        Task<PayFastViewModel> SaveQuoteFromCart(string cartId,
            string sessionUserId);

        Task ProcessOnlinePayment(int id);
    }
}
