using BusinessObject;
using BusinessObject.ViewModel;
using DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class QuoteStatusTransform
    {
        /// <summary>
        /// Convert QuoteStatus Object into QuoteStatus Entity
        /// </summary>
        ///<param name="entity">IQueryable DataAccess.QuoteStatus</param>
        ///<returns>IEnumerable QuoteStatus</returns>
        ///
        public static IEnumerable<QuoteStatusViewModel> ToListViewModel(
            this IQueryable<DataAccess.QuoteStatus> entity)
        {


            return entity.Select(a =>
                        new QuoteStatusViewModel
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Discriminator = a.Discriminator,
                            RequiresPayment = a.RequiresPayment,
                            SessionUserId = a.CreatedUserId,
                            MessageTemplateId =a.MessageTemplateId,
                            IsActive = a.IsActive,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        }).AsEnumerable();
        }

        /// <summary>
        /// Convert QuoteStatus Object into QuoteStatus Entity
        /// </summary>
        ///<param name="model">QuoteStatus</param>
        ///<param name="QuoteStatusEntity">DataAccess.QuoteStatus</param>
        ///<returns>DataAccess.QuoteStatus</returns>
        public static DataAccess.QuoteStatus ToEntity(this QuoteStatusViewModel model,
            DataAccess.QuoteStatus entity)
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
            entity.Name = model.Name;
            entity.Discriminator = model.Discriminator;
            entity.RequiresPayment = model.RequiresPayment;
            entity.MessageTemplateId = model.MessageTemplateId;

            return entity;
        }

        /// <summary>
        /// Convert QuoteStatus Entity  into QuoteStatus Object
        /// </summary>
        ///<param name="model">QuoteStatusViewModel</param>
        ///<param name="QuoteStatusEntity">DataAccess.QuoteStatus</param>
        ///<returns>QuoteStatusViewModel</returns>
        public static QuoteStatusViewModel ToViewModel(
         this DataAccess.QuoteStatus entity,
         QuoteStatusViewModel model)
        {

            model.SessionUserId = entity.CreatedUserId;
            model.Id = entity.Id;
            model.Name = entity.Name;
            model.Discriminator = entity.Discriminator;
            model.RequiresPayment = entity.RequiresPayment;
            model.MessageTemplateId = entity.MessageTemplateId;
            model.MessageTemplate = entity.MessageTemplate.ToViewModel(new MessageTemplateViewModel());
            return model;
        }
    }
}
