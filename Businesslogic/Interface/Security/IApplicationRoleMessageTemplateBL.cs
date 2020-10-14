using BusinessObject.Component;
using BusinessObject.ViewModel;
using System.Threading.Tasks;

namespace BusinessLogic.Interface
{
    public interface IApplicationRoleMessageTemplateBL
    {
        Task<SaveResult> SaveEntityList(MessageTemplateViewModel model, DataAccess.MessageTemplate container);
    }
}
