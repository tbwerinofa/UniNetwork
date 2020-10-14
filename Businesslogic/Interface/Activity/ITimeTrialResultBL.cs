using BusinessObject.Component;
using BusinessObject.ViewModel;
using DataAccess;
using System.Threading.Tasks;

namespace BusinessLogic.Interface
{
    public interface ITimeTrialResultBL
    {
        ResultSetPage<TimeTrialResultViewModel> GetEntityListBySearchParams(
       GridLoadParam paramList,
       TimeTrialResultViewModel model);

    }
}
