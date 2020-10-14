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
    public class SystemDocumentBL : IEntityViewLogic<SystemDocumentViewModel>, ISystemDocumentBL
    {
        protected readonly SqlServerApplicationDbContext _context;
        protected IFeaturedImageBL _featuredImage;
        protected IFinYearBL _finYearBL;
        private readonly IDocumentBL _documentBL;
        #region Constructors
        public SystemDocumentBL(SqlServerApplicationDbContext context,
            IFeaturedImageBL featuredImage,
            IDocumentBL documentBL,
            IFinYearBL finYearBL)
        {
            _context = context;
            _featuredImage = featuredImage;
            _documentBL = documentBL;
            _finYearBL = finYearBL;
        }
        #endregion

        #region Methods

        #region Read

        public ResultSetPage<SystemDocumentViewModel> GetEntityListBySearchParams(
           GridLoadParam paramList)
        {

            var propertyInfo = typeof(SystemDocumentViewModel).GetProperty(paramList.SortField);

            var resultSet = _context.SystemDocument
                 .Include(a => a.Document.DocumentType)
                  .Include(a => a.FinYear)
                 .Include(a => a.UpdatedUser)
                .Include(a => a.CreatedUser)
                 .IgnoreQueryFilters()
                .WhereIf(!String.IsNullOrEmpty(paramList.SearchTerm), a => a.Document.Name.Contains(paramList.SearchTerm))
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }
        public async Task<SystemDocumentViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new SystemDocumentViewModel {
            };

            PopulateDropDowns(viewModel);

            if (Id > 0)
            {
                var entity = await _context.SystemDocument
                      .Include(a => a.Document.DocumentType)
                  .Include(a => a.FinYear)
                    .IgnoreQueryFilters()

                    .Include(c => c.Document)
                      .FirstOrDefaultAsync(a => a.Id == Id);
                if (entity != null)
                {
                    entity.ToViewModel(viewModel);
                    //viewModel.SystemDocumentSizes = entity.SystemDocumentSizes.ToSelectListItem(x => x.Size.ShortName, x => x.SizeId.ToString());
                }
            }
            return viewModel;
        }


        private void PopulateDropDowns(SystemDocumentViewModel model)
        {
            model.FinYears = _finYearBL.GetSelectListItem(includeUpcomingYear:true);
            model.DocumentTypes = _context.DocumentType
                .Where(a=> a.Discriminator == DocumentTypeDiscriminator.ASALicenseForm || a.Discriminator == DocumentTypeDiscriminator.Constitution)
                                          .ToSelectListItem(a => a.Name, x => x.Id.ToString());
        }

        public IEnumerable<SystemDocumentViewModel> GetModelList()
        {

            return _context.SystemDocument
                  .Include(a => a.Document.DocumentType)
                  .Include(a => a.FinYear)
                 .Include(a => a.UpdatedUser)
                .Include(a => a.CreatedUser)
                .IgnoreQueryFilters()
                .AsNoTracking()
                .Where(a => a.Document.DocumentType.Discriminator == DocumentTypeDiscriminator.ASALicenseForm || a.Document.DocumentType.Discriminator == DocumentTypeDiscriminator.Constitution)
                .ToListViewModel();

        }
        #endregion

        #region Create/Update

        public async Task<SaveResult> Manage(SystemDocumentViewModel viewModel)
        {
            return await SaveEntity(viewModel);
        }

        public async Task<SaveResult> SaveEntity(SystemDocumentViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
                Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

                var entity = new SystemDocument();
         
            try
                {

                if (_context.SystemDocument.IgnoreQueryFilters().Any(a => a.Id == viewModel.Id))
                {
                    entity = await _context.SystemDocument.IgnoreQueryFilters()
                .Include(c => c.Document)
                .Include(c => c.Document.DocumentType)
                .Include(c => c.UpdatedUser)
                .Include(c => c.CreatedUser)
                .FirstOrDefaultAsync(a => a.Id == viewModel.Id);

                    entity.Document.DocumentTypeId = viewModel.DocumentTypeId;
                    if (viewModel.DocumentId == 0 && viewModel.FileUploaded != null)
                    {
                        // await UploadDocument(viewModel, entity);
                        await _documentBL.UploadDocument(viewModel.FileUploaded, entity.Document, viewModel.SessionUserId);
                       
                    }
                    entity = viewModel.ToEntity(entity);
                    _context.SystemDocument.Update(entity);
                }
                else
                {

                    entity = viewModel.ToEntity(entity);

                    if (viewModel.FileUploaded != null)
                    {
                        entity.Document = new Document {
                            DocumentTypeId =viewModel.DocumentTypeId
                        };
                        await _documentBL.UploadDocument(viewModel.FileUploaded, entity.Document,viewModel.SessionUserId);
                        viewModel.DocumentId = entity.Document.Id;
                        entity.DocumentId = entity.Document.Id;
                        //await UploadDocument(viewModel, entity);
                    }


                    _context.SystemDocument.Add(entity);

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

                 PopulateDropDowns(viewModel);
                return saveResult;
            }

      


        #endregion

        #region Delete
        public async Task<SaveResult> DeleteEntity(int Id)
        {
            SaveResult resultSet = new SaveResult();
            Dictionary<bool, string> dictionary = new Dictionary<bool, string>();
            try
            {
                var entity = await _context.SystemDocument.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == Id);
                _context.SystemDocument.Remove(entity);
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
