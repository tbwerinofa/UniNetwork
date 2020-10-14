using BusinessObject;
using DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class SubscriptionTypeAttributeTransform
	{
		/// <summary>
		/// Convert SubscriptionTypeAttribute Object into SubscriptionTypeAttribute Entity
		/// </summary>
		///<param name="entity">IQueryable DataAccess.SubscriptionTypeAttribute</param>
		///<returns>IEnumerable SubscriptionTypeAttribute</returns>
		///
		public static IEnumerable<SubscriptionTypeAttributeViewModel> ToListViewModel(
			this IQueryable<DataAccess.SubscriptionTypeAttribute> entity)
		{
            
			return entity
			.ToList()
			.Select(a =>
						new SubscriptionTypeAttributeViewModel
						{
							Id = a.Id,
							SubscriptionTypeId = a.SubscriptionTypeId,
                            SubscriptionType = a.SubscriptionType.Name,
                            Name = a.Name,
                            //LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            //LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName,
                        });
		}

		/// <summary>
		/// Convert SubscriptionTypeAttribute Object into SubscriptionTypeAttribute Entity
		/// </summary>
		///<param name="model">SubscriptionTypeAttribute</param>
		///<param name="SubscriptionTypeAttributeEntity">DataAccess.SubscriptionTypeAttribute</param>
		///<returns>DataAccess.SubscriptionTypeAttribute</returns>
		public static DataAccess.SubscriptionTypeAttribute ToEntity(this SubscriptionTypeAttributeViewModel model,
            DataAccess.SubscriptionTypeAttribute entity)
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

			entity.SubscriptionTypeId = model.SubscriptionTypeId;
			entity.Name = model.Name;
			return entity;
		}

		/// <summary>
		/// Convert SubscriptionTypeAttribute Entity  into SubscriptionTypeAttribute Object
		/// </summary>
		///<param name="model">SubscriptionTypeAttributeViewModel</param>
		///<param name="SubscriptionTypeAttributeEntity">DataAccess.SubscriptionTypeAttribute</param>
		///<returns>SubscriptionTypeAttributeViewModel</returns>
		public static SubscriptionTypeAttributeViewModel ToViewModel(
		 this DataAccess.SubscriptionTypeAttribute entity,
		 SubscriptionTypeAttributeViewModel model)
		{

			model.SessionUserId = entity.CreatedUserId;
			model.Id = entity.Id;
            model.SubscriptionTypeId = entity.SubscriptionTypeId;
			model.SubscriptionType = entity.SubscriptionType.Name;
            model.Name = entity.Name;

			return model;
		}
	}
}
