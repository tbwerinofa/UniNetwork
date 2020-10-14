using BusinessObject.Component;
using BusinessObject.ViewModel;
using DataAccess;
using System.Threading.Tasks;

namespace BusinessLogic.Interface
{
    public interface IRaceResultMappingBL
    {
        Task<SaveResult> SaveEntityList(RaceResultViewModel model, RaceDistance container);

    }
}
