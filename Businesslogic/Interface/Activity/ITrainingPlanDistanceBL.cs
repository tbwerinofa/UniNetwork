using BusinessObject.Component;
using BusinessObject.ViewModel;
using DataAccess;
using System.Threading.Tasks;

namespace BusinessLogic.Interface
{
    public interface ITrainingPlanDistanceBL
    {
        Task<SaveResult> SaveEntityList(TrainingPlanViewModel model, TrainingPlan container);

    }
}
