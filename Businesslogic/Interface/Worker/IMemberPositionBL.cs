using BusinessObject;
using BusinessObject.Component;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Interface
{
    public interface IMemberPositionBL
    {

        IEnumerable<DropDownListItems> GetEntityDropDownListItems_ByMNCPlantIds(
            IEnumerable<int> mncPlantIds,
            int capacityBuildingId);

        IEnumerable<MemberPositionViewModel> GetListViewModel(string search);

        IEnumerable<SelectListItem> GetSelectListItem_ForTypeAhead(string query);

        Task<MemberPositionViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null);

        ResultSetPage<MemberPositionViewModel> GetVerificationList(
           GridLoadParam paramList);
    }
}
