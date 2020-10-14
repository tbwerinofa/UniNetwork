using BusinessObject;
using BusinessObject.Component;
using System.Threading.Tasks;

namespace BusinessLogic.Interface
{
    public interface IFeaturedImageBL
    {
        Task<SaveResult> SaveEntity(ProductImageViewModel viewModel);

    }
}
