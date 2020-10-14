using BusinessObject;
using BusinessObject.Component;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Interface
{
    public interface IMemberStagingBL
    {
        Task<SaveResult> VerifyModel(MemberStagingViewModel viewModel);
    }
}
