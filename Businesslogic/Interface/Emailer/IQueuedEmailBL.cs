using BusinessObject;
using BusinessObject.Component;
using BusinessObject.ViewModel;
using DataAccess;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Interface
{
    public interface IQueuedEmailBL
    {
        SaveResult GenerateRegistrationEmail(MemberStagingViewModel model, string messageBody);

        Task<SaveResult> GenerateAccountEmail(string fullName, string email, string messageBody, string messageTemplate, string sessionUserId);

        Task SendQueuedEmail();

        Task<SaveResult> GenerateRegistrationNotificationEmail(MemberStagingViewModel model,string messageTemplate);



    }
}
