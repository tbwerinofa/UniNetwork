using BusinessObject;
using BusinessObject.Component;
using DataAccess;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Interface
{
    public interface IMemberBL
    {

        IEnumerable<DropDownListItems> GetEntityDropDownListItems_ByGenderId(
          int genderId);

        IEnumerable<SelectListItem> GetEntitySelectListItem_ByAwardGenderId(
         int awardId);

        IEnumerable<MemberViewModel> GetEntityList();

        Task<MemberViewModel> GetEntityById(
               int? Id, AuthorizationModel model = null);

        Task<MemberViewModel> GetEntityByUserId(
       string userId);

        Task<SaveResult> SaveEntity(MemberViewModel viewModel);

        Task<SaveResult> DeleteEntity(int Id);

        IEnumerable<SelectListItem> GetSelectListItem(bool excludeDefault =false);
    }
}
