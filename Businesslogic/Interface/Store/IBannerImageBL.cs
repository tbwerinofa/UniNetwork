using BusinessObject;
using BusinessObject.Component;
using System.Threading.Tasks;

namespace BusinessLogic.Interface
{
    public interface IBannerImageBL
    {
        Task<SaveResult> Manage(BannerImageViewModel viewModel);

        Task<BannerImageViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null);

    }
}
