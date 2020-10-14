using BusinessObject.Component;
using BusinessObject.ViewModel;
using DataAccess;
using System.Threading.Tasks;

namespace BusinessLogic.Interface
{
    public interface ITimeTrialResultMappingBL
    {
        Task<SaveResult> SaveEntityList(TimeTrialResultViewModel model, TimeTrialDistance container);

    }
}
