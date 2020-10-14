using BusinessObject;
using BusinessObject.Component;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public interface IEntityViewLogic<T>
    {
        ResultSetPage<T> GetEntityListBySearchParams(GridLoadParam paramList);
        Task<T> GetEntityById(int? Id,AuthorizationModel model = null);

        //IEnumerable<T> GetEntityList(string search);

        Task<SaveResult> SaveEntity(T viewModel);
        Task<SaveResult> DeleteEntity(int Id);

    }
}
