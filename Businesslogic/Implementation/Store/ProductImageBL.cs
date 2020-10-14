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
    public class ProductImageBL : IEntityViewLogic<ProductImageViewModel>, IProductImageBL
    {
        protected readonly SqlServerApplicationDbContext _context;
        protected IFeaturedImageBL _featuredImage;
        private readonly IDocumentBL _documentBL;
        #region Constructors
        public ProductImageBL(SqlServerApplicationDbContext context,
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

        public ResultSetPage<ProductImageViewModel> GetEntityListBySearchParams(
           GridLoadParam paramList)
        {

            var propertyInfo = typeof(ProductImageViewModel).GetProperty(paramList.SortField);

            var resultSet = _context.ProductImage
                .Include(a=>a.Product.ProductCategory)
                 .Include(a => a.Document)
                 .Include(a => a.UpdatedUser)
                .Include(a => a.CreatedUser)
                 .IgnoreQueryFilters()
                .WhereIf(!String.IsNullOrEmpty(paramList.SearchTerm), a => a.Document.Name.Contains(paramList.SearchTerm))
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }
        public async Task<ProductImageViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new ProductImageViewModel {
            };

            PopulateDropDowns(viewModel);

            if (Id > 0)
            {
                var entity = await _context.ProductImage
                    .IgnoreQueryFilters()
                    .Include(c => c.Product.ProductCategory)
                    .Include(c => c.Document)
                     .Include(c => c.FeaturedImages).ThenInclude(b=> b.ProductImage.Product)
                      .Include(c => c.FeaturedImages).ThenInclude(b => b.ProductImage.Document)
                      .FirstOrDefaultAsync(a => a.Id == Id);
                if (entity != null)
                {
                    entity.ToViewModel(viewModel);
                    //viewModel.ProductImageSizes = entity.ProductImageSizes.ToSelectListItem(x => x.Size.ShortName, x => x.SizeId.ToString());
                }
            }
            return viewModel;
        }

        private void PopulateDropDowns(ProductImageViewModel model)
        {
            model.Products = _context.Product.ToSelectListItem(a => a.Name, x => x.Id.ToString());
            model.FeaturedCategories = _context.FeaturedCategory.ToListViewModel();
        }



        public IEnumerable<SelectListItem> GetSelectListItems()
        {

            return _context.ProductImage
                .ToSelectListItem(x => x.Product.Name.ToString(),
                                                     x => x.Id.ToString());
        }



        #endregion

        #region Create/Update

        public async Task<SaveResult> Manage(ProductImageViewModel viewModel)
        {
            return await SaveEntity(viewModel);
        }

        public async Task<SaveResult> SaveEntity(ProductImageViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
                Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

                var entity = new ProductImage();
         
            try
                {

                if (_context.ProductImage.IgnoreQueryFilters().Any(a => a.Id == viewModel.Id))
                {
                    entity = await _context.ProductImage.IgnoreQueryFilters()
                .Include(c => c.Document)
                .Include(c => c.Document.DocumentType)
                .Include(c => c.UpdatedUser)
                .Include(c => c.CreatedUser)
                .FirstOrDefaultAsync(a => a.Id == viewModel.Id);


                    if (viewModel.DocumentId == 0 && viewModel.FileUploaded != null)
                    {
                        //await UploadDocument(viewModel, entity);
                        await _documentBL.UploadDocument(viewModel.FileUploaded, entity.Document, viewModel.SessionUserId, DocumentTypeDiscriminator.ProductImage);
                        viewModel.DocumentId = entity.Document.Id;
                    }
                    entity = viewModel.ToEntity(entity);
                    _context.ProductImage.Update(entity);
                }
                else
                {

                    entity = viewModel.ToEntity(entity);

                    if (viewModel.FileUploaded != null)
                    {
                        entity.Document = new Document();
                        await _documentBL.UploadDocument(viewModel.FileUploaded, entity.Document, viewModel.SessionUserId, DocumentTypeDiscriminator.ProductImage);
                        // await UploadDocument(viewModel, entity);
                    }


                    _context.ProductImage.Add(entity);

                }
                await _context.SaveChangesAsync();
                if (entity.Id > 0)
                {
                    saveResult.IsSuccess = true;
                    saveResult.Id = entity.Id;
                    viewModel.Id = entity.Id;

                    if (viewModel.IsFeatured)
                    {
                         await UpdateIsFeaturedField(
                                                    viewModel.Id,
                                                   viewModel.ProductId);
                    }

                    if (saveResult.IsSuccess)
                    {

                        saveResult = await _featuredImage.SaveEntity(viewModel);
                    }
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

            if(!saveResult.IsSuccess)
            {
                PopulateDropDowns(viewModel);
            }
                return saveResult;
            }

        private async Task UploadDocument(ProductImageViewModel viewModel, ProductImage entity)
        {
            try
            {


                var docType = await _context.DocumentType.FirstOrDefaultAsync(a => a.Discriminator == DocumentTypeDiscriminator.ProductImage);

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

        private async Task UpdateIsFeaturedField(
        int productImageId,
        int productId)
        {
            
            List<DataAccess.ProductImage> productImageList = new List<DataAccess.ProductImage>();

            var entityList = _context.ProductImage.Where(a=> a.ProductId == productId && a.Id != productImageId);

            foreach (var item in entityList)
            {
                item.IsFeatured = false;

                productImageList.Add(item);
            }

            if (productImageList.Any()) { 
                await _context.ProductImage.AddRangeAsync(productImageList);
                await _context.SaveChangesAsync();
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
                var entity = await _context.ProductImage.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == Id);
                _context.ProductImage.Remove(entity);
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
