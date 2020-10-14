using BusinessObject;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Interface
{
    public interface IDocumentBL
    {
        Task UploadDocument(IFormFile formFile, Document entity, string sessionUserId);
        Task UploadDocument(IFormFile formFile, Document entity, string sessionUserId,string documentTypeDiscr);

        Task DeleteEntity(int documentId);
    }
}
