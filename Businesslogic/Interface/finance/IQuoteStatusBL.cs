using BusinessObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Interface
{
    public interface IQuoteStatusBL
    {
        IEnumerable<QuoteStatusViewModel> GetEntityList();

        Task<QuoteStatusViewModel> GetEntityById(
            int? Id,
            AuthorizationModel model = null);

        Task<QuoteStatusViewModel> GetEntityByDiscr(
       string discriminator);
    }
}
