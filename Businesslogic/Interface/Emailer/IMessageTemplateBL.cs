using BusinessObject.Component;
using BusinessObject.ViewModel;
using DataAccess;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Interface
{
    public interface IMessageTemplateBL
    {
        IEnumerable<MessageTemplateViewModel> GetMessageTemplates();

        IQueryable<DataAccess.MessageTemplate> GetRegistrationTemplates(IEnumerable<string> messageTemplates);

        Task<MessageTemplate> GetEntityByName(string messageTemplateName);
    }
}
