using BusinessObject;
using BusinessObject.Component;
using DataAccess;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BusinessLogic.Interface
{
    public interface IPersonBL
    {
       Task<SaveResult> GenerateEntity(MemberStagingViewModel viewModel);
        void UpdateEntity(MemberViewModel viewModel, Member member);

        Task<int> GetMemberIdAsync(int personId);

        Task<Person> GetPersonIdAsync(string personGuid);

        Task<SaveResult> UploadPicture(IFormFile Files,string personGuid);

        Task<SaveResult> DeletePicture(string personGuid);
    }
}
