using BusinessLogic.Interface;
using BusinessObject;
using BusinessObject.Component;
using DataAccess;
using DomainObjects;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transform;

namespace BusinessLogic.Implementation
{
    public class ProductBL : IEntityViewLogic<ProductViewModel>,IProductBL
    {
        protected readonly SqlServerApplicationDbContext _context;
        protected  IProductSizeBL _productSize;
        #region Constructors
        public ProductBL(SqlServerApplicationDbContext context,
            IProductSizeBL productSize)
        {
            _context = context;
            _productSize = productSize;
        }
        #endregion

        #region Methods

        #region Read

        public ResultSetPage<ProductViewModel> GetEntityListBySearchParams(
           GridLoadParam paramList)
        {

            var propertyInfo = typeof(ProductViewModel).GetProperty(paramList.SortField);

            var resultSet = _context.Product
                .Include(a=>a.ProductCategory)
                 .Include(a => a.ProductImages)
                 .Include(a => a.UpdatedUser)
                .Include(a => a.CreatedUser)
                 .IgnoreQueryFilters()
                .WhereIf(!String.IsNullOrEmpty(paramList.SearchTerm), a => a.Name.Contains(paramList.SearchTerm))
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }
        public async Task<ProductViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new ProductViewModel {
            };

            PopulateDropDowns(viewModel);

            if (Id > 0)
            {
                var entity = await _context.Product
                    .IgnoreQueryFilters()
                    .Include(c => c.ProductCategory)
                    .Include(c => c.ProductSizes).ThenInclude(d=>d.Size)
                      .FirstOrDefaultAsync(a => a.Id == Id);
                if (entity != null)
                {
                    entity.ToViewModel(viewModel);
                    viewModel.ProductSizes = entity.ProductSizes.ToSelectListItem(x => x.Size.ShortName, x => x.SizeId.ToString());
                }
            }
            return viewModel;
        }

        private void PopulateDropDowns(ProductViewModel model)
        {
            model.Sizes = _context.Size.ToListViewModel();
            model.ProductCategories = _context.ProductCategory.ToSelectListItem(a => a.Name, x => x.Id.ToString());
        }

        public IEnumerable<SelectListItem> GetSelectListItems()
        {

            return _context.Product
                .ToSelectListItem(x => x.Name.ToString(),
                                                     x => x.Id.ToString());
        }

        public IEnumerable<SelectListItem> GetEntitySelectListItem_ByParentId(
          int parentId)
        {
            var selectListItem = this._context.Product.Where(a => a.ProductCategoryId == parentId).ToSelectListItem(a => a.Name, x => x.Id.ToString()).OrderBy(a => a.Text);

            return selectListItem;
        }

        #endregion

        #region Create/Update
        public async Task<SaveResult> SaveEntity(ProductViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
                Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

                var entity = new Product();
         
            try
                {

                    if (viewModel.Id != 0)
                    {
                        if (_context.Product.IgnoreQueryFilters().Any(a => a.Id == viewModel.Id))
                        {
                            entity = await _context.Product
                            .IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == viewModel.Id);
                        }
                        entity = viewModel.ToEntity(entity);
                       _context.Product.Update(entity);
                    }
                    else
                    {
                        entity = viewModel.ToEntity(entity);
                        _context.Product.Add(entity);
                    }

                    await _context.SaveChangesAsync();


                    if (entity.Id > 0)
                    {
                        saveResult.IsSuccess = true;
                        saveResult.Id = entity.Id;
                        viewModel.Id = entity.Id;
                        saveResult = await _productSize.SaveProductSize(viewModel);
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


        #endregion

        #region Delete
        public async Task<SaveResult> DeleteEntity(int Id)
        {
            SaveResult resultSet = new SaveResult();
            Dictionary<bool, string> dictionary = new Dictionary<bool, string>();
            try
            {
                var entity = await _context.Product.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == Id);
                _context.Product.Remove(entity);
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
