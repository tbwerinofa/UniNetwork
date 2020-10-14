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
using System.Linq;
using System.Threading.Tasks;
using Transform;

namespace BusinessLogic.Implementation
{
    public class ProductCategoryBL : IEntityViewLogic<ProductCategoryViewModel>
    {
        protected readonly SqlServerApplicationDbContext _context;

        #region Constructors
        public ProductCategoryBL(SqlServerApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods

        #region Read

        public ResultSetPage<ProductCategoryViewModel> GetEntityListBySearchParams(
           GridLoadParam paramList)
        {

            var propertyInfo = typeof(ProductCategoryViewModel).GetProperty(paramList.SortField);

            var data = _context.ProductCategory;

            var resultSet = _context.ProductCategory
                 .IgnoreQueryFilters()
                .WhereIf(!String.IsNullOrEmpty(paramList.SearchTerm), a => a.Name.Contains(paramList.SearchTerm))
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }
        public async Task<ProductCategoryViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new ProductCategoryViewModel();

            PopulateDropDowns(viewModel);

            if (Id > 0)
            {
                var entity = await _context.ProductCategory
                    .IgnoreQueryFilters()
                      .FirstOrDefaultAsync(a => a.Id == Id);
                if (entity != null)
                {
                    entity.ToViewModel(viewModel);
                }
            }
            return viewModel;
        }

        private void PopulateDropDowns(ProductCategoryViewModel model)
        {
        }

        public IEnumerable<SelectListItem> GetSelectListItems()
        {

            return _context.ProductCategory
                .ToSelectListItem(x => x.Name.ToString(),
                                                     x => x.Id.ToString());
        }



        #endregion

        #region Create/Update
        public async Task<SaveResult> SaveEntity(ProductCategoryViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
                Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

                var entity = new ProductCategory();
         
            try
                {

                    if (viewModel.Id != 0)
                    {
                        if (_context.ProductCategory.IgnoreQueryFilters().Any(a => a.Id == viewModel.Id))
                        {
                            entity = await _context.ProductCategory.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == viewModel.Id);
                        }
                        entity = viewModel.ToEntity(entity);
                        _context.ProductCategory.Update(entity);
                    }
                    else
                    {
                        entity = viewModel.ToEntity(entity);
                        _context.ProductCategory.Add(entity);
                    }

                    await _context.SaveChangesAsync();

                    if (entity.Id > 0)
                    {
                        saveResult.IsSuccess = true;
                        saveResult.Id = entity.Id;
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
                var entity = await _context.ProductCategory.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == Id);
                _context.ProductCategory.Remove(entity);
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
