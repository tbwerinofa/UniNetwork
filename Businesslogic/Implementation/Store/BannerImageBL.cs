using BusinessLogic.Interface;
using BusinessObject;
using BusinessObject.Component;
using BusinessObject.ViewModel;
using DataAccess;
using DomainObjects;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Transform;

namespace BusinessLogic.Implementation
{
    public class BannerImageBL : IEntityViewLogic<BannerImageViewModel>, IBannerImageBL
    {
        protected readonly SqlServerApplicationDbContext _context;
        protected IFeaturedImageBL _featuredImage;
        private readonly IDocumentBL _documentBL;
        #region Constructors
        public BannerImageBL(SqlServerApplicationDbContext context,
            IFeaturedImageBL featuredImage,
            IDocumentBL documentBL)
        {
            _context = context;
            _featuredImage = featuredImage;
            _documentBL = documentBL;
        }
        #endregion

        #region Methods

        #region Read

        public ResultSetPage<BannerImageViewModel> GetEntityListBySearchParams(
           GridLoadParam paramList)
        {

            var propertyInfo = typeof(BannerImageViewModel).GetProperty(paramList.SortField);

            var resultSet = _context.BannerImage
                 .Include(a => a.Document)
                 .Include(a => a.UpdatedUser)
                .Include(a => a.CreatedUser)
                 .IgnoreQueryFilters()
                .WhereIf(!String.IsNullOrEmpty(paramList.SearchTerm), a => a.Document.Name.Contains(paramList.SearchTerm))
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }
        public async Task<BannerImageViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new BannerImageViewModel {
            };



            if (Id > 0)
            {
                var entity = await _context.BannerImage
                    .IgnoreQueryFilters()

                    .Include(c => c.Document)
                      .FirstOrDefaultAsync(a => a.Id == Id);
                if (entity != null)
                {
                    entity.ToViewModel(viewModel);
                    //viewModel.BannerImageSizes = entity.BannerImageSizes.ToSelectListItem(x => x.Size.ShortName, x => x.SizeId.ToString());
                }
            }
            return viewModel;
        }



        #endregion

        #region Create/Update

        public async Task<SaveResult> Manage(BannerImageViewModel viewModel)
        {
            return await SaveEntity(viewModel);
        }

        public async Task<SaveResult> SaveEntity(BannerImageViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
                Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

                var entity = new BannerImage();
         
            try
                {

                if (_context.BannerImage.IgnoreQueryFilters().Any(a => a.Id == viewModel.Id))
                {
                    entity = await _context.BannerImage.IgnoreQueryFilters()
                .Include(c => c.Document)
                .Include(c => c.Document.DocumentType)
                .Include(c => c.UpdatedUser)
                .Include(c => c.CreatedUser)
                .FirstOrDefaultAsync(a => a.Id == viewModel.Id);


                    if (viewModel.DocumentId == 0 && viewModel.FileUploaded != null)
                    {
                        // await UploadDocument(viewModel, entity);
                        await _documentBL.UploadDocument(viewModel.FileUploaded, entity.Document, viewModel.SessionUserId, DocumentTypeDiscriminator.BannerImage);
                       
                    }
                    entity = viewModel.ToEntity(entity);
                    _context.BannerImage.Update(entity);
                }
                else
                {

                    entity = viewModel.ToEntity(entity);

                    if (viewModel.FileUploaded != null)
                    {
                        entity.Document = new Document ();
                        await _documentBL.UploadDocument(viewModel.FileUploaded, entity.Document,viewModel.SessionUserId, DocumentTypeDiscriminator.BannerImage);
                        viewModel.DocumentId = entity.Document.Id;
                        //await UploadDocument(viewModel, entity);
                    }


                    _context.BannerImage.Add(entity);

                }
                await _context.SaveChangesAsync();
                if (entity.Id > 0)
                {
                    saveResult.IsSuccess = true;
                    saveResult.Id = entity.Id;
                    viewModel.Id = entity.Id;

                }
            }
                catch (DbUpdateException upDateEx)
                {
                    var results = upDateEx.GetSqlerrorNo();
                    string msg = results == (int)SqlErrNo.FK ? ConstEntity.MissingValueMsg : ConstEntity.UniqueKeyMsg;
                    saveResult = dictionary.GetValidateEntityResults(msg).ToSaveResult();

                }
                catch (Exception ex)
                {

                    saveResult.Message = CrudError.SaveErrorMsg;
                }


                return saveResult;
            }

        private async Task UploadDocument(BannerImageViewModel viewModel, BannerImage entity)
        {
            try
            {


                var docType = await _context.DocumentType.FirstOrDefaultAsync(a => a.Discriminator == DocumentTypeDiscriminator.BannerImage);

                if (docType == null)
                {
                    throw new ArgumentNullException(nameof(DocumentType));
                }

                var document = new DocumentViewModel
                {
                    SessionUserId = viewModel.SessionUserId,
                    DocumentTypeId = docType.Id,
                    DocumentNameGuid = Guid.NewGuid().ToString()
                };

                if (entity.DocumentId > 0)
                {
                    viewModel.DocumentId = entity.DocumentId;
                }

                using (var ms = new MemoryStream())
                {
                    viewModel.FileUploaded.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    string s = Convert.ToBase64String(fileBytes);
                    document.DocumentData = fileBytes;
                    var parsedContentDisposition = ContentDispositionHeaderValue.Parse(viewModel.FileUploaded.ContentDisposition);
                    document.Name = parsedContentDisposition.FileName.TrimStart('\"').TrimEnd('\"');
                }


                document.ToEntity(entity.Document);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region Delete
        public async Task<SaveResult> DeleteEntity(int Id)
        {
            SaveResult resultSet = new SaveResult();
            Dictionary<bool, string> dictionary = new Dictionary<bool, string>();
            try
            {
                var entity = await _context.BannerImage.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == Id);
                _context.BannerImage.Remove(entity);
                await _context.SaveChangesAsync();

                resultSet.IsSuccess = true;
            }
            catch (DbUpdateException upDateEx)
            {
                var results = upDateEx.GetSqlerrorNo();

                string msg = results == (int)SqlErrNo.FK ? ConstEntity.ForeignKeyDelMsg : CrudError.DeleteErrorMsg;
                resultSet = dictionary.GetValidateEntityResults(msg).ToSaveResult();

            }
            catch (Exception ex)
            {

                resultSet.Message = CrudError.DeleteErrorMsg;
            }
            return resultSet;

        }
        #endregion

        #endregion
     
    }
}
