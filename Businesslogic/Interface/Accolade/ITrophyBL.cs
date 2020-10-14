using BusinessObject;
using BusinessObject.Component;
using BusinessObject.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interface
{
    public interface ITrophyBL
    {
        Task<SaveResult> Manage(TrophyViewModel viewModel);

        Task<TrophyViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null);

    }
}
