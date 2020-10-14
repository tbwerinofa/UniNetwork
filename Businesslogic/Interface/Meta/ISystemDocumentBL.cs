using BusinessObject;
using BusinessObject.Component;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Interface
{
    public interface ISystemDocumentBL
    {
        Task<SaveResult> Manage(SystemDocumentViewModel viewModel);

        Task<SystemDocumentViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null);

        IEnumerable<SystemDocumentViewModel> GetModelList();
    }
}
