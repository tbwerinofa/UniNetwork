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
    public class ItemBL : IEntityViewLogic<ItemViewModel>
    {

        #region Global Region
        protected readonly SqlServerApplicationDbContext _context;
        #endregion


        #region Constructors
        public ItemBL(SqlServerApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods

        #region Read

        public ResultSetPage<ItemViewModel> GetEntityListBySearchParams(
           GridLoadParam paramList)
        {

            var propertyInfo = typeof(ItemViewModel).GetProperty(paramList.SortField);

            var resultSet = _context.Item
                .Include(a=>a.ProductSize.Product.ProductCategory)
                  .Include(a => a.ProductSize.Size)
                 .Include(a => a.UpdatedUser)
                .Include(a => a.CreatedUser)
                 .IgnoreQueryFilters()
                .WhereIf(!String.IsNullOrEmpty(paramList.SearchTerm), a => a.ProductSize.Product.Name.Contains(paramList.SearchTerm))
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }
        public async Task<ItemViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new ItemViewModel {
            };

            PopulateDropDowns(viewModel);

            if (Id > 0)
            {
                var entity = await _context.Item
                    .IgnoreQueryFilters()
                   .Include(a => a.ProductSize.Product.ProductCategory)
                  .Include(a => a.ProductSize.Size)
                      .FirstOrDefaultAsync(a => a.Id == Id);
                if (entity != null)
                {
                    entity.ToViewModel(viewModel);
                }
            }
            return viewModel;
        }

        private void PopulateDropDowns(ItemViewModel model)
        {
            model.ProductCategories = _context.ProductCategory.ToSelectListItem(a => a.Name, x => x.Id.ToString());
            model.Products = IQueryableExtensions.Default_SelectListItem();
            model.ProductSizes = IQueryableExtensions.Default_SelectListItem();
        }



        public IEnumerable<SelectListItem> GetSelectListItems()
        {

            return _context.Item
                .ToSelectListItem(x => x.ProductSize.Product.Name.ToString(),
                                                     x => x.Id.ToString());
        }



        #endregion

        #region Create/Update
        public async Task<SaveResult> SaveEntity(ItemViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
                Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

                var entity = new Item();
         
            try
                {

                    if (viewModel.Id != 0)
                    {
                        if (_context.Item.IgnoreQueryFilters().Any(a => a.Id == viewModel.Id))
                        {
                            entity = await _context.Item
                            .IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == viewModel.Id);
                        }
                        entity = viewModel.ToEntity(entity);
                       _context.Item.Update(entity);
                    }
                    else
                    {
                        entity = viewModel.ToEntity(entity);
                        _context.Item.Add(entity);
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


        #endregion

        #region Delete
        public async Task<SaveResult> DeleteEntity(int Id)
        {
            SaveResult resultSet = new SaveResult();
            Dictionary<bool, string> dictionary = new Dictionary<bool, string>();
            try
            {
                var entity = await _context.Item.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == Id);
                _context.Item.Remove(entity);
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
