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
    public class SubscriptionTypeBL : IEntityViewLogic<SubscriptionTypeViewModel>, ISubscriptionTypeBL
    {
        protected readonly SqlServerApplicationDbContext _context;


        #region Constructors
        public SubscriptionTypeBL(SqlServerApplicationDbContext context)
        {
            _context = context;
        }


        #region Methods

        #region Read

        public ResultSetPage<SubscriptionTypeViewModel> GetEntityListBySearchParams(
          GridLoadParam paramList)
        {

            var propertyInfo = typeof(SubscriptionTypeViewModel).GetProperty(paramList.SortField);

            var resultSet = _context.SubscriptionType
                .Include(a => a.CreatedUser)
                .Include(a => a.UpdatedUser)
                .Include(a => a.SubscriptionTypeRules).ThenInclude(x => x.SubscriptionTypeRuleAudits).ThenInclude(b => b.QuoteDetails).ThenInclude(c => c.Quote)
                 .IgnoreQueryFilters()
                .WhereIf(!string.IsNullOrEmpty(paramList.SearchTerm), a => a.Name.Contains(paramList.SearchTerm))
                .ToListViewModel(paramList.SessionUserId);

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }
        public async Task<SubscriptionTypeViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new SubscriptionTypeViewModel();

            if (Id > 0)
            {
                var entity = await _context.SubscriptionType
                    .IgnoreQueryFilters()
                    .FirstOrDefaultAsync(a => a.Id == Id);

                viewModel = entity.ToViewModel(viewModel);
            }

            return viewModel;
        }

        public IEnumerable<SelectListItem> GetSelectListItems()
        {

            return _context.SubscriptionType
                .ToSelectListItem(x => x.Name.ToString(),
                                                     x => x.Id.ToString());
        }

        public IEnumerable<SubscriptionTypeViewModel> GetEntityList(
          string userId)
        {
            var entity = _context.SubscriptionType;
            var model = entity.Include(a=> a.SubscriptionTypeAttributes).ThenInclude(b => b.SubscriptionType)
                .Include(a=> a.SubscriptionTypeRules).ThenInclude(b=>b.SubscriptionTypeRuleAudits).ThenInclude(b => b.QuoteDetails).ThenInclude(c => c.Quote)
                .Include(a => a.SubscriptionTypeRules).ThenInclude(b => b.AgeGroup)
                .Include(a => a.CreatedUser)
                .Include(a => a.UpdatedUser)
                .ToListViewModel(userId);

            return model;
        }
        #endregion

        #region Create/Update
        public async Task<SaveResult> SaveEntity(SubscriptionTypeViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
                Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

                var entity = new SubscriptionType();
                try
                {
                    if (viewModel.Id != 0)
                    {
                        if (_context.SubscriptionType.IgnoreQueryFilters().Any(a => a.Id == viewModel.Id))
                        {
                            entity = await _context.SubscriptionType.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == viewModel.Id);
                        }
                        entity = viewModel.ToEntity(entity);
                        _context.SubscriptionType.Update(entity);
                    }
                    else
                    {
                        entity = viewModel.ToEntity(entity);
                        _context.SubscriptionType.Add(entity);
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
                var entity = await _context.SubscriptionType.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == Id);
                _context.SubscriptionType.Remove(entity);
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
