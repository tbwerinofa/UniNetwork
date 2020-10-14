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
    public class SubscriptionTypeAttributeBL : IEntityViewLogic<SubscriptionTypeAttributeViewModel>
    {
        #region Global fields
        protected readonly SqlServerApplicationDbContext _context;
        #endregion

        #region Constructors
        public SubscriptionTypeAttributeBL(SqlServerApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods

        #region Read

        public ResultSetPage<SubscriptionTypeAttributeViewModel> GetEntityListBySearchParams(
          GridLoadParam paramList)
        {

            var propertyInfo = typeof(SubscriptionTypeAttributeViewModel).GetProperty(paramList.SortField);

            var resultSet = _context.SubscriptionTypeAttribute
                .Include(a=> a.SubscriptionType)
                .Include(a => a.CreatedUser)
                .Include(a => a.UpdatedUser)
                 .IgnoreQueryFilters()
                .WhereIf(!string.IsNullOrEmpty(paramList.SearchTerm), a => a.Name.Contains(paramList.SearchTerm))
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }
        public async Task<SubscriptionTypeAttributeViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new SubscriptionTypeAttributeViewModel();

            PopulateDropDowns(viewModel);

            if (Id > 0)
            {
                var entity = await _context.SubscriptionTypeAttribute
                    .IgnoreQueryFilters()
                    .FirstOrDefaultAsync(a => a.Id == Id);

                viewModel = entity.ToViewModel(viewModel);
            }

            return viewModel;
        }

        private void PopulateDropDowns(SubscriptionTypeAttributeViewModel model)
        {
            model.SubscriptionTypes = _context.SubscriptionType.ToSelectListItem(a => a.Name, x => x.Id.ToString());
        }
        public IEnumerable<SelectListItem> GetSelectListItems()
        {

            return _context.SubscriptionTypeAttribute
                .ToSelectListItem(x => x.Name.ToString(),
                                                     x => x.Id.ToString());
        }


        #endregion

        #region Create/Update
        public async Task<SaveResult> SaveEntity(SubscriptionTypeAttributeViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
                Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

                var entity = new SubscriptionTypeAttribute();
                try
                {
                    if (viewModel.Id != 0)
                    {
                        if (_context.SubscriptionTypeAttribute.IgnoreQueryFilters().Any(a => a.Id == viewModel.Id))
                        {
                            entity = await _context.SubscriptionTypeAttribute.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == viewModel.Id);
                        }
                        entity = viewModel.ToEntity(entity);
                        _context.SubscriptionTypeAttribute.Update(entity);
                    }
                    else
                    {
                        entity = viewModel.ToEntity(entity);
                        _context.SubscriptionTypeAttribute.Add(entity);
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
                var entity = await _context.SubscriptionTypeAttribute.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == Id);
                _context.SubscriptionTypeAttribute.Remove(entity);
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
