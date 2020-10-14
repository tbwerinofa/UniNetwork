using BusinessObject.Component;
using BusinessObject.ViewModel;
using DataAccess;
using System.Threading.Tasks;

namespace BusinessLogic.Interface
{
    public interface IModeratorBL
    {
        Task<SaveResult> SaveEntityList(CalendarViewModel model, Calendar container);

    }
}
