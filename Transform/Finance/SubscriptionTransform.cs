using BusinessObject;
using DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class SubscriptionTransform
    {
        /// <summary>
        /// Convert Subscription Object into Subscription Entity
        /// </summary>
        ///<param name="entity">IQueryable DataAccess.Subscription</param>
        ///<returns>IEnumerable Subscription</returns>
        ///
        public static IEnumerable<SubscriptionViewModel> ToListViewModel(
            this IQueryable<DataAccess.Subscription> entity)
        {
            DateTime currentDate = DateTime.Now;
            return entity.ToList().Select(a =>
                        new SubscriptionViewModel
                        {
                            Id = a.Id,
                            QuoteDetailId = a.QuoteDetailId,
                            StartDate = a.StartDate,
                            EndDate = a.EndDate,
                            EndDateLongDate = a.EndDate.ToCustomLongDate(),
                            StartDateLongDate = a.StartDate.ToCustomLongDate(),
                            HasExpired = a.EndDate < currentDate?true: false,
                            QuoteNo = a.QuoteDetail.Quote.QuoteNo,
                            SubscriptionType =  a.QuoteDetail.SubscriptionTypeRuleAudit.SubscriptionTypeRule.SubscriptionType.Name,
                            FullName = a.Member.Person.FullName,
                            IsActive = a.IsActive,
                            SessionUserId = a.CreatedUserId,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        }).AsEnumerable();
        }

        /// <summary>
        /// Convert Subscription Object into Subscription Entity
        /// </summary>
        ///<param name="model">Subscription</param>
        ///<param name="SubscriptionEntity">DataAccess.Subscription</param>
        ///<returns>DataAccess.Subscription</returns>
        public static DataAccess.Subscription ToEntity(this SubscriptionViewModel model,
            DataAccess.Subscription entity)
        {
            if (entity.Id == 0)
            {
                entity.CreatedUserId = model.SessionUserId;
            }
            else
            {
                entity.UpdatedUserId = model.SessionUserId;
                entity.UpdatedTimestamp = DateTime.Now;
            }
            entity.QuoteDetailId = model.QuoteDetailId;
            entity.StartDate = model.StartDate;
            entity.EndDate = model.EndDate;

            return entity;
        }

        /// <summary>
        /// Convert Subscription Entity  into Subscription Object
        /// </summary>
        ///<param name="model">SubscriptionViewModel</param>
        ///<param name="SubscriptionEntity">DataAccess.Subscription</param>
        ///<returns>SubscriptionViewModel</returns>
        public static SubscriptionViewModel ToViewModel(
         this DataAccess.Subscription entity,
         SubscriptionViewModel model)
        {

            model.SessionUserId = entity.CreatedUserId;
            model.Id = entity.Id;
            model.QuoteDetailId = entity.QuoteDetailId;
            model.StartDate = entity.StartDate;
            model.EndDate = entity.EndDate;
            model.EndDateLongDate = entity.EndDate.ToCustomLongDate();
            model.StartDateLongDate = entity.StartDate.ToCustomLongDate();
            model.QuoteNo = entity.QuoteDetail.Quote.QuoteNo;
            model.SubscriptionType = entity.QuoteDetail.SubscriptionTypeRuleAudit.SubscriptionTypeRule.SubscriptionType.Name;
            model.FullName = entity.QuoteDetail.Quote.QuoteUser.FirstName + " " + entity.QuoteDetail.Quote.QuoteUser.Surname;
            model.SubscriptionUserId = entity.QuoteDetail.Quote.QuoteUserId;
            model.SubscriptionTypeModel = entity.QuoteDetail.SubscriptionTypeRuleAudit.SubscriptionTypeRule.SubscriptionType.ToViewModel(new SubscriptionTypeViewModel());

            return model;
        }

        public static IEnumerable<SubscriptionViewModel> ToQueryListViewModel(
    this IQueryable<DataAccess.Subscription> entity)
        {
            DateTime currentDate = DateTime.Now;
            return entity.ToList().Select(a =>
                        new SubscriptionViewModel
                        {
                            Id = a.Id,
                            QuoteDetailId = a.QuoteDetailId,
                            StartDate = a.StartDate,
                            EndDate = a.EndDate,
                            EndDateLongDate = a.EndDate.ToCustomLongDate(),
                            StartDateLongDate = a.StartDate.ToCustomLongDate(),
                            HasExpired = a.EndDate < currentDate ? true : false,
                            QuoteNo = a.QuoteDetail.Quote.QuoteNo,
                            SubscriptionType = a.QuoteDetail.SubscriptionTypeRuleAudit.SubscriptionTypeRule.SubscriptionType.Name,
                            FullName = a.QuoteDetail.Quote.QuoteUser.FirstName + " " + a.QuoteDetail.Quote.QuoteUser.Surname,
                            SessionUserId = a.CreatedUserId,
                            SubscriptionTypeModel = a.QuoteDetail.SubscriptionTypeRuleAudit.SubscriptionTypeRule.SubscriptionType.ToViewModel(new SubscriptionTypeViewModel())
        }).AsEnumerable();
        }
    }
}
