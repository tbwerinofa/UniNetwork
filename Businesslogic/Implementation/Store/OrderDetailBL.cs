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
    public class OrderDetailBL : IEntityViewLogic<OrderDetailViewModel>
    {
        protected readonly SqlServerApplicationDbContext _context;

        #region Constructors
        public OrderDetailBL(SqlServerApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods

        #region Read

        public ResultSetPage<OrderDetailViewModel> GetEntityListBySearchParams(
           GridLoadParam paramList)
        {

            var propertyInfo = typeof(OrderDetailViewModel).GetProperty(paramList.SortField);

            var resultSet = _context.OrderDetail
                .Include(a=>a.Quote)
                 .Include(a => a.ProductSize.Size)
                 .Include(a => a.ProductSize.Product)
                 .Include(a => a.UpdatedUser)
                .Include(a => a.CreatedUser)
                 .IgnoreQueryFilters()
                .WhereIf(!String.IsNullOrEmpty(paramList.SearchTerm), a => a.ProductSize.Product.Name.Contains(paramList.SearchTerm))
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }
        public async Task<OrderDetailViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new OrderDetailViewModel {
            };

            PopulateDropDowns(viewModel);

            if (Id > 0)
            {
                var entity = await _context.OrderDetail
                    .IgnoreQueryFilters()
                    .Include(a => a.ProductSize.Size)
                 .Include(a => a.ProductSize.Product)
                      .FirstOrDefaultAsync(a => a.Id == Id);
                if (entity != null)
                {
                    entity.ToViewModel(viewModel);
                }
            }
            return viewModel;
        }

        private void PopulateDropDowns(OrderDetailViewModel model)
        {
            //model.Sizes = _context.Size.ToListViewModel();
            //model.OrderDetailCategories = _context.OrderDetailCategory.ToSelectListItem(a => a.Name, x => x.Id.ToString());
        }

        //public IEnumerable<SelectListItem> GetSelectListItems()
        //{

        //    return _context.OrderDetail
        //        .ToSelectListItem(x => x.Name.ToString(),
        //                                             x => x.Id.ToString());
        //}

        //public IEnumerable<SelectListItem> GetEntitySelectListItem_ByParentId(
        //  int parentId)
        //{
        //    var selectListItem = this._context.OrderDetail.Where(a => a.OrderDetailCategoryId == parentId).ToSelectListItem(a => a.Name, x => x.Id.ToString()).OrderBy(a => a.Text);

        //    return selectListItem;
        //}

        #endregion

        #region Create/Update
        public async Task<SaveResult> SaveEntity(OrderDetailViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
                Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

                var entity = new OrderDetail();
         
            try
                {

                    if (viewModel.Id != 0)
                    {
                        if (_context.OrderDetail.IgnoreQueryFilters().Any(a => a.Id == viewModel.Id))
                        {
                            entity = await _context.OrderDetail
                            .IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == viewModel.Id);
                        }
                        entity = viewModel.ToEntity(entity);
                       _context.OrderDetail.Update(entity);
                    }
                    else
                    {
                        entity = viewModel.ToEntity(entity);
                        _context.OrderDetail.Add(entity);
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
                var entity = await _context.OrderDetail.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == Id);
                _context.OrderDetail.Remove(entity);
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
