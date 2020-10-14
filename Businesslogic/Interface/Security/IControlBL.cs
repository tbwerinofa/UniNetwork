using BusinessObject;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Interface
{
    public interface IControlBL
    {
        Task<string> GetControlByType(string type);
    }
}
