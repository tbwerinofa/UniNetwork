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
    public class SubscriptionBL : IEntityViewLogic<SubscriptionViewModel>,ISubscriptionBL
    {
        protected readonly SqlServerApplicationDbContext _context;


        #region Constructors
        public SubscriptionBL(SqlServerApplicationDbContext context)
        {
            _context = context;
        }


        #region Methods

        #region Read

        public ResultSetPage<SubscriptionViewModel> GetEntityListBySearchParams(
          GridLoadParam paramList)
        {

            var propertyInfo = typeof(SubscriptionViewModel).GetProperty(paramList.SortField);

            var resultSet = _context.Subscription
                .Include(a => a.QuoteDetail.Quote.QuoteUser)
                    .Include(a => a.QuoteDetail.SubscriptionTypeRuleAudit.SubscriptionTypeRule.SubscriptionType)
                      .Include(a => a.Member.Person)
                    .Include(a=>a.UpdatedUser)
                    .Include(a => a.CreatedUser)
                 .IgnoreQueryFilters()
                .WhereIf(!string.IsNullOrEmpty(paramList.SearchTerm), a => a.QuoteDetail.SubscriptionTypeRuleAudit.SubscriptionTypeRule.SubscriptionType.Name.Contains(paramList.SearchTerm))
                .ToListViewModel();

            return paramList.ToResultSetPage(propertyInfo, resultSet);

        }
        public async Task<SubscriptionViewModel> GetEntityById(
              int? Id, AuthorizationModel model = null)
        {

            var viewModel = new SubscriptionViewModel();

            if (Id > 0)
            {
                var entity = await _context.Subscription
                    .Include(a=>a.QuoteDetail.Quote.QuoteUser)
                    .Include(a => a.QuoteDetail.SubscriptionTypeRuleAudit.SubscriptionTypeRule.SubscriptionType)
                    .IgnoreQueryFilters()
                    .FirstOrDefaultAsync(a => a.Id == Id);

                viewModel = entity.ToViewModel(viewModel);
            }

            return viewModel;
        }


        public IEnumerable<SubscriptionViewModel> GetEntityList(
          string userId)
        {
            var entity = _context.Subscription;
            var model = entity.Include(a=> a.SubscriptionHistories)
                .Include(a=> a.QuoteDetail)
                 .Include(a => a.Member.Person)
                .ToListViewModel();

            return model;
        }

        public IEnumerable<SubscriptionTypeRuleAuditViewModel> GetEntityByType_UserId(
        string userId,
        int? subscriptionTypeId,
        int? personId)
        {

           var ageGroupEntity = _context.Person
                .Include(a => a.AgeGroup)
                .Include(a => a.Members).ThenInclude(a => a.MemberMappings).ThenInclude(a => a.RelationMember.Person)
                .FirstOrDefault(b => b.Id == personId);

            var members = ageGroupEntity.Members.SelectMany(b => b.MemberMappings).ToSelectListItem(a => a.RelationMember.Person.FullName, a => a.MemberId.ToString(),true).ToList();

            var model = _context.SubscriptionTypeRuleAudit
                .Include(a=>a.AgeGroup)
                  .Include(a => a.QuoteDetails).ThenInclude(a => a.Subscriptions)
                .Where(a=>a.AgeGroupId.HasValue &&a.AgeGroupId == ageGroupEntity.AgeGroupId || !a.AgeGroupId.HasValue)
            .ToListViewModel(members);

            return model;
        }

        #endregion

        #region Create/Update
        public async Task<SaveResult> SaveEntity(SubscriptionViewModel viewModel)
        {
            SaveResult saveResult = new SaveResult();
                Dictionary<bool, string> dictionary = new Dictionary<bool, string>();

                var entity = new Subscription();
                try
                {
                    if (viewModel.Id != 0)
                    {
                        if (_context.Subscription.IgnoreQueryFilters().Any(a => a.Id == viewModel.Id))
                        {
                            entity = await _context.Subscription.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == viewModel.Id);
                        }
                        entity = viewModel.ToEntity(entity);
                        _context.Subscription.Update(entity);
                    }
                    else
                    {
                        entity = viewModel.ToEntity(entity);
                        _context.Subscription.Add(entity);
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

        public void GenerateList(
                 QuoteViewModel model,
                 DataAccess.Quote quote)
        {

            foreach (var item in quote.QuoteDetails)
            {

                var subscription = new DataAccess.Subscription
                {
                    CreatedUserId = model.SessionUserId,
                    QuoteDetailId = item.Id,
                    MemberId = model.ParentMemberId,
                    StartDate = DateTime.Now,
                };

                if (item.SubscriptionTypeRuleAudit.ActiveMonths.HasValue)
                {
                    DateTime endDate = (subscription.StartDate).AddMonths((int)item.SubscriptionTypeRuleAudit.ActiveMonths);
                    subscription.EndDate = endDate;
                }

                var subscriptionHistory = new DataAccess.SubscriptionHistory
                {
                    CreatedUserId = model.SessionUserId,
                    QuoteDetailId = item.Id,
                    MemberId = model.ParentMemberId,
                    SubscriptionId = subscription.Id,
                    StartDate = subscription.StartDate,
                    EndDate = subscription.EndDate
                };

                subscription.SubscriptionHistories.Add(subscriptionHistory);
                item.Subscriptions.Add(subscription);

                if (item.SubscriptionTypeRuleAudit.HasRelations && model.MemberListIds != null)
                {

                    foreach (var memberId in model.MemberListIds)
                    {
                        subscription = new DataAccess.Subscription
                        {
                            CreatedUserId = model.SessionUserId,
                            QuoteDetailId = item.Id,
                            MemberId = memberId,
                            StartDate = DateTime.Now,
                        };

                        if (item.SubscriptionTypeRuleAudit.ActiveMonths.HasValue)
                        {
                            DateTime endDate = (subscription.StartDate).AddMonths((int)item.SubscriptionTypeRuleAudit.ActiveMonths);
                            subscription.EndDate = endDate;
                        }

                        subscriptionHistory = new DataAccess.SubscriptionHistory
                        {
                            CreatedUserId = model.SessionUserId,
                            QuoteDetailId = item.Id,
                            MemberId = memberId,
                            SubscriptionId = subscription.Id,
                            StartDate = subscription.StartDate,
                            EndDate = subscription.EndDate
                        };

                        subscription.SubscriptionHistories.Add(subscriptionHistory);
                        item.Subscriptions.Add(subscription);

                    }
                    //all relations
                    //generate subscriptions
                    //generate subscriptions history
                }
            };


        }

        #endregion

        #region Delete
        public async Task<SaveResult> DeleteEntity(int Id)
        {
            SaveResult resultSet = new SaveResult();
            Dictionary<bool, string> dictionary = new Dictionary<bool, string>();
            try
            {
                var entity = await _context.Subscription.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == Id);
                _context.Subscription.Remove(entity);
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
