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
    public interface IProductImageBL
    {
        Task<SaveResult> Manage(ProductImageViewModel viewModel);

        Task<ProductImageViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null);

    }
}
