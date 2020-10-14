using BusinessObject;
using DomainObjects;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class SubscriptionTypeRuleAuditTransform
    {
		/// <summary>
		/// Convert SubscriptionTypeRule Object into SubscriptionTypeRule Entity
		/// </summary>
		///<param name="entity">IQueryable DataAccess.SubscriptionTypeRule</param>
		///<returns>IEnumerable SubscriptionTypeRule</returns>
		///
		public static IEnumerable<SubscriptionTypeRuleAuditViewModel> ToListViewModel(
			this IQueryable<DataAccess.SubscriptionTypeRuleAudit> entity,
            IEnumerable<SelectListItem> memberMapping)
		{
            DateTime currentDate = DateTime.Now;
            return entity
                .Include(a => a.SubscriptionTypeRule.SubscriptionType)
            .ToList()
            .Select(a =>
                        new SubscriptionTypeRuleAuditViewModel
                        {
                            Id = a.Id,
                            SubscriptionTypeRuleId = a.SubscriptionTypeRuleId,
                            SubscriptionTypeId = a.SubscriptionTypeRule.SubscriptionTypeId,
                            AgeGroup = a.AgeGroup?.Name,
                            SubscriptionType = a.SubscriptionTypeRule.SubscriptionType.Name,
                            AmountRand = a.AmountRand,
                            ActiveMonths = a.ActiveMonths,
                            SessionUserId = a.CreatedUserId,
                            HasQuantity = a.HasQuantity,
                            HasRelations = a.HasRelations,
                            MemberMappings = memberMapping,
                            HasSubscription = a.QuoteDetails.Any(x => x.Subscriptions.Any(b=>b.EndDate?.AddMonths(-1) > currentDate))
                            //QuoteFullName = organisation.Any()? organisation.First().UserFullName:string.Empty,
                            //QuoteUserId = organisation.Any() ? organisation.First().UserId : string.Empty
                        });
		}

		/// <summary>
		/// Convert SubscriptionTypeRuleAudit Object into SubscriptionTypeRuleAudit Entity
		/// </summary>
		///<param name="model">SubscriptionTypeRuleAudit</param>
		///<param name="SubscriptionTypeRuleAuditEntity">DataAccess.SubscriptionTypeRuleAudit</param>
		///<returns>DataAccess.SubscriptionTypeRuleAudit</returns>
		public static DataAccess.SubscriptionTypeRuleAudit ToSubscriptionTypeRuleAuditEntity(this DataAccess.SubscriptionTypeRuleAudit entity,
			SubscriptionTypeRuleAuditViewModel model)
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

			entity.SubscriptionTypeRuleId = model.SubscriptionTypeRuleId;
			entity.AmountRand = model.AmountRand;
			entity.ActiveMonths = model.ActiveMonths;
            entity.HasQuantity = model.HasQuantity;
            entity.HasRelations = model.HasRelations;
            return entity;
		}

		/// <summary>
		/// Convert SubscriptionTypeRuleAudit Entity  into SubscriptionTypeRuleAudit Object
		/// </summary>
		///<param name="model">SubscriptionTypeRuleAuditViewModel</param>
		///<param name="SubscriptionTypeRuleAuditEntity">DataAccess.SubscriptionTypeRuleAudit</param>
		///<returns>SubscriptionTypeRuleAuditViewModel</returns>
		public static SubscriptionTypeRuleAuditViewModel ToSubscriptionTypeRuleAuditViewModel(
		 this DataAccess.SubscriptionTypeRuleAudit entity,
		 SubscriptionTypeRuleAuditViewModel model)
		{

			model.SessionUserId = entity.CreatedUserId;
			model.Id = entity.Id;
			model.SubscriptionType = entity.SubscriptionTypeRule.SubscriptionType.Name;			
			model.SubscriptionTypeId = entity.SubscriptionTypeRule.SubscriptionTypeId;
			model.AmountRand = entity.AmountRand;
			model.ActiveMonths = entity.ActiveMonths;
            model.HasQuantity = entity.HasQuantity;
            model.HasRelations = entity.HasRelations;
            return model;
		}


        public static DataAccess.SubscriptionTypeRuleAudit ToAuditEntity(
            this DataAccess.SubscriptionTypeRule entity)
        {
            return new DataAccess.SubscriptionTypeRuleAudit {

                CreatedUserId = entity.CreatedUserId,
                SubscriptionTypeRuleId = entity.Id,
                AmountRand = entity.AmountRand,
                ActiveMonths = entity.ActiveMonths,
                HasQuantity = entity.HasQuantity,
                AgeGroupId = entity.AgeGroupId,
                IsActive = true
        };
        }


    }
}
