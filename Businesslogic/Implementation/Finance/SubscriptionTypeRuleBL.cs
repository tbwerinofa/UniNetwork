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
    public class SubscriptionTypeRuleBL : IEntityViewLogic<SubscriptionTypeRuleViewModel>
    {
        #region Global fields
        protected readonly SqlServerApplicationDbContext _context;
        #endregion

        #region Constructors
        public SubscriptionTypeRuleBL(SqlServerApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods

        #region Read

        public ResultSetPage<SubscriptionTypeRuleViewModel> GetEntityListBySearchParams(
          GridLoadParam paramList)
        {

            var propertyInfo = typeof(SubscriptionTypeRuleViewModel).GetProperty(paramList.SortField);

            var resultSet = _context.SubscriptionTypeRule
                .Include(a=> a.SubscriptionType)
                .Include(a => a.AgeGroup)
                .Include(a => a.CreatedUser)
                .Include(a => a.UpdatedUser)
                 .IgnoreQueryFilters()
                .WhereIf(!string.IsNullOrEmpty(paramList.SearchTerm), a => a.SubscriptionType.Name.Contains(paramList.SearchTerm))
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }
        public async Task<SubscriptionTypeRuleViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new SubscriptionTypeRuleViewModel();

            PopulateDropDowns(viewModel);

            if (Id > 0)
            {
                var entity = await _context.SubscriptionTypeRule
                    .IgnoreQueryFilters()
                    .FirstOrDefaultAsync(a => a.Id == Id);

                viewModel = entity.ToViewModel(viewModel);
            }

            return viewModel;
        }

        private void PopulateDropDowns(SubscriptionTypeRuleViewModel model)
        {
            model.SubscriptionTypes = _context.SubscriptionType.ToSelectListItem(a => a.Name, x => x.Id.ToString());
            model.AgeGroups = _context.AgeGroup.ToSelectListItem(a => a.Name, x => x.Id.ToString());
        }
        public IEnumerable<SelectListItem> GetSelectListItems()
        {

            return _context.SubscriptionTypeRule
                .ToSelectListItem(x => x.SubscriptionType.Name.ToString(),
                                                     x => x.Id.ToString());
        }


        #endregion

        #region Create/Update
        public async Task<SaveResult> SaveEntity(SubscriptionTypeRuleViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
                Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

                var entity = new SubscriptionTypeRule();
                try
                {
                    if (viewModel.Id != 0)
                    {
                        if (_context.SubscriptionTypeRule.IgnoreQueryFilters().Any(a => a.Id == viewModel.Id))
                        {
                            entity = await _context.SubscriptionTypeRule
                            .Include(a=>a.SubscriptionTypeRuleAudits)
                            .IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == viewModel.Id);
                        }

                         MapToAudit(entity,viewModel);
                        _context.SubscriptionTypeRule.Update(entity);
                    }
                    else
                    {
                    MapToAudit(entity, viewModel);
                    _context.SubscriptionTypeRule.Add(entity);
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


        private void MapToAudit(SubscriptionTypeRule dbentity,SubscriptionTypeRuleViewModel model)
        {

            bool hasChanged = false;

            if (
            (dbentity.AmountRand != model.AmountRand) ||
            (dbentity.ActiveMonths != model.ActiveMonths)
            )
            {
                hasChanged = true;
            }


            var entity = model.ToEntity(dbentity);


            entity.SubscriptionTypeRuleAudits
                .ToList()
                .ForEach(a =>
                a.IsActive = false);

            if (hasChanged)
            {
                var audit = entity.ToAuditEntity();
                entity.SubscriptionTypeRuleAudits.Add(audit);
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
                var entity = await _context.SubscriptionTypeRule.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == Id);
                _context.SubscriptionTypeRule.Remove(entity);
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
