using BusinessObject;
using BusinessObject.Component;
using BusinessObject.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Interface
{
    public interface IPayFastNotifyBL
    {
        Task<SaveResult> UpdateStatus(
         DataAccess.PayFastNotify entity);

        Task<SaveResult> SaveEntity(PayFastNotifyViewModel viewModel);
    }
}
