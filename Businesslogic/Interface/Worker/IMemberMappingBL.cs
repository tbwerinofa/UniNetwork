using BusinessObject;
using BusinessObject.Component;
using BusinessObject.ViewModel;
using DataAccess;
using System.Threading.Tasks;

namespace BusinessLogic.Interface
{
    public interface IMemberMappingBL
    {
        Task<SaveResult> SaveEntityList(RegisterViewModel model, Member parentEntity);
        Task<SaveResult> SaveEntityList(MemberViewModel model, Member parentEntity);
    }
}
