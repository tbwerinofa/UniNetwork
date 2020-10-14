using BusinessLogic.Interface;
using BusinessObject;
using BusinessObject.Component;
using BusinessObject.ViewModel;
using DataAccess;
using DomainObjects;
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
    public class TrophyBL : IEntityViewLogic<TrophyViewModel>, ITrophyBL
    {
        protected readonly SqlServerApplicationDbContext _context;


        #region Constructors
        public TrophyBL(SqlServerApplicationDbContext context)
        {
            _context = context;
        }


        #region Methods

        #region Read

        public ResultSetPage<TrophyViewModel> GetEntityListBySearchParams(
          GridLoadParam paramList)
        {
            try
            {

           
            var propertyInfo = typeof(TrophyViewModel).GetProperty(paramList.SortField);

            var trophy = _context.Trophy.ToList();

            var resultSet = _context.Trophy
                 .IgnoreQueryFilters()
                .WhereIf(!String.IsNullOrEmpty(paramList.SearchTerm), a => a.Document.Name.Contains(paramList.SearchTerm))
                 .IgnoreQueryFilters()
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public async Task<TrophyViewModel> GetEntityById(
              int? Id,
              AuthorizationModel authorizationFilter = null)
        {

            var viewModel = new TrophyViewModel();

            if (Id > 0)
            {

                var entity = await _context.Trophy
                     .IgnoreQueryFilters()
                    .Include(c => c.Document)
                    .Include(c => c.Document.DocumentType)
                    .Include(c => c.UpdatedUser)
                    .Include(c => c.CreatedUser)
                    .FirstOrDefaultAsync(a => a.Id == Id);
                if (entity != null)
                {
                    entity.ToViewModel(viewModel);
                }
            }
            return viewModel;
        }



        #endregion

        #region Create/Update

        public async Task<SaveResult> Manage(TrophyViewModel viewModel)
        {
            return await SaveEntity(viewModel);
        }
        public async Task<SaveResult> SaveEntity(TrophyViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
            Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

            var entity = new Trophy();
            try
            {
                if (_context.Trophy.IgnoreQueryFilters().Any(a => a.Id == viewModel.Id))
                {
                    entity = await _context.Trophy.IgnoreQueryFilters()
                .Include(c => c.Document)
                .Include(c => c.Document.DocumentType)
                .Include(c => c.UpdatedUser)
                .Include(c => c.CreatedUser)
                .FirstOrDefaultAsync(a => a.Id == viewModel.Id);


                    if (!viewModel.DocumentId.HasValue && viewModel.FileUploaded!=null)
                    {
                        if (entity.Document == null)
                        {
                            entity.Document = new Document();
                        }
                        await UploadDocument(viewModel, entity);
                    }
                    entity = viewModel.ToEntity(entity);
                    _context.Trophy.Update(entity);
                }
                else
                {

                    entity = viewModel.ToEntity(entity);

                    if (viewModel.FileUploaded != null)
                    {
                        entity.Document = new Document();
                        await UploadDocument(viewModel, entity);
                    }


                    _context.Trophy.Add(entity);

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

               
                   if(upDateEx.InnerException.Message.Contains("IX_Document_Name"))
                        {
                        msg = "Document with the same name already exists";
                    }
               
                saveResult = dictionary.GetValidateEntityResults(msg).ToSaveResult();

            }
            catch (Exception ex)
            {

                saveResult.Message = CrudError.SaveErrorMsg;
            }


            return saveResult;
        }

        private async Task UploadDocument(TrophyViewModel viewModel, Trophy entity)
        {
            try
            {


                var docType = await _context.DocumentType.FirstOrDefaultAsync(a => a.Discriminator == DocumentTypeDiscriminator.Trophy);

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
                var entity = await _context.Trophy.IgnoreQueryFilters().Include(a=> a.Document).FirstOrDefaultAsync(a=> a.Id == Id);
                _context.Document.Remove(entity.Document);
                _context.Trophy.Remove(entity);
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
        #endregion
    }
}
