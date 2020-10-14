using BusinessObject;
using DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class SubscriptionTypeRuleTransform
    {
        /// <summary>
        /// Convert SubscriptionTypeRule Object into SubscriptionTypeRule Entity
        /// </summary>
        ///<param name="entity">IQueryable DataAccess.SubscriptionTypeRule</param>
        ///<returns>IEnumerable SubscriptionTypeRule</returns>
        ///
        public static IEnumerable<SubscriptionTypeRuleViewModel> ToListViewModel(
            this IQueryable<DataAccess.SubscriptionTypeRule> entity)
        {
            
            return entity
            .ToList()
            .Select(a =>
                        new SubscriptionTypeRuleViewModel
                        {
                            Id = a.Id,
                            SubscriptionTypeId = a.SubscriptionTypeId,
                            HasQuantity = a.HasQuantity,
                            SubscriptionType = a.SubscriptionType.Name,
                            AgeGroup = a.AgeGroup?.Name,
                            AmountRand = a.AmountRand,
                            ActiveMonths = a.ActiveMonths,
                            IsActive = a.IsActive,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        });
        }

        /// <summary>
        /// Convert SubscriptionTypeRule Object into SubscriptionTypeRule Entity
        /// </summary>
        ///<param name="model">SubscriptionTypeRule</param>
        ///<param name="SubscriptionTypeRuleEntity">DataAccess.SubscriptionTypeRule</param>
        ///<returns>DataAccess.SubscriptionTypeRule</returns>
        public static DataAccess.SubscriptionTypeRule ToEntity(this SubscriptionTypeRuleViewModel model,
            DataAccess.SubscriptionTypeRule entity)
        {
            if (entity.Id == 0)
            {
                entity.CreatedUserId = model.SessionUserId;
                entity.IsActive = model.IsActive;
            }
            else
            {
                entity.UpdatedUserId = model.SessionUserId;
                entity.UpdatedTimestamp = DateTime.Now;
                
            }

            entity.SubscriptionTypeId = model.SubscriptionTypeId;
            entity.AgeGroupId = model.AgeGroupId == 0 ? null : model.AgeGroupId;
            entity.AmountRand = model.AmountRand;
            entity.ActiveMonths = model.ActiveMonths;
            entity.HasQuantity = model.HasQuantity;
            entity.HasRelations = model.HasRelations;

            return entity;
        }

        /// <summary>
        /// Convert SubscriptionTypeRule Entity  into SubscriptionTypeRule Object
        /// </summary>
        ///<param name="model">SubscriptionTypeRuleViewModel</param>
        ///<param name="SubscriptionTypeRuleEntity">DataAccess.SubscriptionTypeRule</param>
        ///<returns>SubscriptionTypeRuleViewModel</returns>
        public static SubscriptionTypeRuleViewModel ToViewModel(
         this DataAccess.SubscriptionTypeRule entity,
         SubscriptionTypeRuleViewModel model)
        {

            model.SessionUserId = entity.CreatedUserId;
            model.Id = entity.Id;
            model.SubscriptionType = entity.SubscriptionType.Name;
            model.SubscriptionTypeId = entity.SubscriptionTypeId;
            model.AgeGroupId = entity.AgeGroupId;
            model.AmountRand = entity.AmountRand;
            model.ActiveMonths = entity.ActiveMonths;
            model.HasQuantity = entity.HasQuantity;
            model.HasRelations = entity.HasRelations;
            return model;
        }

        public static IEnumerable<SubscriptionTypeRuleViewModel> ToQueryListViewModel(
        this IQueryable<DataAccess.SubscriptionTypeRule> entity)
        {

            return entity
            .ToList()
            .Select(a =>
                        new SubscriptionTypeRuleViewModel
                        {
                            Id = a.Id,
                            SubscriptionTypeId = a.SubscriptionTypeId,
                            HasQuantity = a.HasQuantity,
                            SubscriptionType = a.SubscriptionType.Name,
                            AgeGroup = a.AgeGroup?.Name,
                            AmountRand = a.AmountRand,
                            ActiveMonths = a.ActiveMonths,
                            HasRelations = a.HasRelations,
                            IsActive = a.IsActive,
                        });
        }
    }
}
