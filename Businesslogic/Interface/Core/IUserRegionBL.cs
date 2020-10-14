using BusinessObject.Component;
using BusinessObject.ViewModel;
using DataAccess;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interface
{
    public interface IUserRegionBL
    {
        Task<SaveResult> SaveEntityList(UserViewModel model, ApplicationUser user);

    }
}
