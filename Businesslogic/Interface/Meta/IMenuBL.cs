using BusinessObject;
using System.Collections.Generic;

namespace BusinessLogic.Interface
{
    public interface IMenuBL
    {
        IEnumerable<MenuViewModel> GetEntityListBy_RoleList(
             IEnumerable<string> roleList,
             DataAccess.ApplicationUser sessionUser,
             string menuArea);
    }
}
