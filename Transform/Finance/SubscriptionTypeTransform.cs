
using BusinessObject;
using DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
	public static class SubscriptionTypeTransform
	{
		/// <summary>
		/// Convert SubscriptionType Object into SubscriptionType Entity
		/// </summary>
		///<param name="entity">IQueryable DataAccess.SubscriptionType</param>
		///<returns>IEnumerable SubscriptionType</returns>
		///
		public static IEnumerable<SubscriptionTypeViewModel> ToListViewModel(
			this IQueryable<DataAccess.SubscriptionType> entity,
            string userId)
		{
            return entity
            .ToList()
            .Select(a =>
                        new SubscriptionTypeViewModel
                        {
                            Id = a.Id,
                            Name = a.Name,
                            SessionUserId = a.CreatedUserId,
                            AmountRand = a.SubscriptionTypeRules.Sum(b => b.SubscriptionTypeRuleAudits.Where(c => c.IsActive).Sum(d => d.AmountRand)),
                            ActiveMonths = a.SubscriptionTypeRules.Sum(b => b.SubscriptionTypeRuleAudits.Where(c => c.IsActive).Sum(d => b.ActiveMonths ?? 0)),
                            SubscriptionTypeAttributeList = a.SubscriptionTypeAttributes.AsQueryable().ToListViewModel(),
                            SubscriptionTypeRuleList = a.SubscriptionTypeRules.AsQueryable().ToQueryListViewModel(),
                            IsActive = a.IsActive,
                            Discriminator = a.Discriminator,
                            HasSubscription = a.SubscriptionTypeRules.Any(x => x.SubscriptionTypeRuleAudits.Any(b => b.QuoteDetails.Where(c => c.Quote.QuoteUserId == userId).Any(c => c.Subscriptions.Any()))),
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName

                        });
		}

		/// <summary>
		/// Convert SubscriptionType Object into SubscriptionType Entity
		/// </summary>
		///<param name="model">SubscriptionType</param>
		///<param name="SubscriptionTypeEntity">DataAccess.SubscriptionType</param>
		///<returns>DataAccess.SubscriptionType</returns>
		public static DataAccess.SubscriptionType ToEntity(this SubscriptionTypeViewModel model,
            DataAccess.SubscriptionType entity
			)
		{
			if (entity.Id == 0)
			{
				entity.CreatedUserId = model.SessionUserId;
                entity.Discriminator = model.Discriminator;
            }
			else
			{
				entity.UpdatedUserId = model.SessionUserId;
				entity.UpdatedTimestamp = DateTime.Now;
			}

			entity.Name = model.Name;
            


            return entity;
		}

		/// <summary>
		/// Convert SubscriptionType Entity  into SubscriptionType Object
		/// </summary>
		///<param name="model">SubscriptionTypeViewModel</param>
		///<param name="SubscriptionTypeEntity">DataAccess.SubscriptionType</param>
		///<returns>SubscriptionTypeViewModel</returns>
		public static SubscriptionTypeViewModel ToViewModel(
		 this DataAccess.SubscriptionType entity,
		 SubscriptionTypeViewModel model)
		{

			model.SessionUserId = entity.CreatedUserId;
			model.Id = entity.Id;
			model.Name = entity.Name;
            model.AmountRand = entity.SubscriptionTypeRules.Sum(a => a.AmountRand);
            model.ActiveMonths = entity.SubscriptionTypeRules.Sum(a => a.ActiveMonths??0);
            model.Discriminator = entity.Discriminator;
            model.AmountRandFormatted = model.AmountRand.ToMonetaryValue();
            return model;
		}
	}
}
