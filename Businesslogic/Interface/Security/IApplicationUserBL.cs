using BusinessObject.Component;
using BusinessObject.ViewModel;
using System.Threading.Tasks;

namespace BusinessLogic.Interface
{
    public interface IApplicationUserBL
    {
        ResultSetPage<UserViewModel> GetEntityListBySearchParams(
          GridLoadParam paramList);
        Task<UserViewModel> GetEntityById(
           string Id);
    }
}
