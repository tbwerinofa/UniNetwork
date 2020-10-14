using BusinessObject.Component;
using BusinessObject.ViewModel;
using DataAccess;
using System.Threading.Tasks;

namespace BusinessLogic.Interface
{
    public interface IRaceResultBL
    {
        ResultSetPage<RaceResultViewModel> GetEntityListBySearchParams(
       GridLoadParam paramList,
       RaceResultViewModel model);

    }
}
