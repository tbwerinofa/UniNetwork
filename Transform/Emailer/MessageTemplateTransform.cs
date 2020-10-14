using BusinessObject.ViewModel;
using DataAccess;
using DomainObjects;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class MessageTemplateTransform
    {
        /// <summary>
        /// Convert MessageTemplate Object into MessageTemplate Entity
        /// </summary>
        ///<param name="entity">IQueryable DataAccess.MessageTemplate</param>
        ///<returns>IEnumerable MessageTemplate</returns>
        ///
        public static IEnumerable<MessageTemplateViewModel> ToListViewModel(
            this IQueryable<DataAccess.MessageTemplate> entity)
        {
            return entity
               .Include(c => c.UpdatedUser)
                       .Include(c => c.CreatedUser)
                    .Select(a =>
                        new MessageTemplateViewModel
                        {
                            Id = a.Id,
                            Subject = a.Subject,
                            Name = a.Name,
                            BccEmailAddresses = a.BccEmailAddresses,
                            FromAddress = a.FromAddress,
                            Body = a.Body,
                            DelayBeforeSend = a.DelayBeforeSend,
                            DelayHours = a.DelayHours,
                            IsActive = a.IsActive,
                            EmailAccountId = a.EmailAccountId,
                            SessionUserId = a.CreatedUserId,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        });
        }

        /// <summary>
        /// Convert MessageTemplate Object into MessageTemplate Entity
        /// </summary>
        ///<param name="model">MessageTemplate</param>
        ///<param name="MessageTemplateEntity">DataAccess.MessageTemplate</param>
        ///<returns>DataAccess.MessageTemplate</returns>
        public static MessageTemplate ToEntity(this MessageTemplateViewModel model,
            MessageTemplate entity)
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
            entity.Subject = model.Subject;
            entity.Name = model.Name;
            entity.BccEmailAddresses = model.BccEmailAddresses;
            entity.Body = model.Body;
            entity.DelayBeforeSend = model.DelayBeforeSend;
            entity.DelayHours = model.DelayHours;
            entity.EmailAccountId = model.EmailAccountId;
            entity.Name = model.Name;
            entity.FromAddress = model.FromAddress;

            return entity;
        }

        /// <summary>
        /// Convert MessageTemplate Entity  into MessageTemplate Object
        /// </summary>
        ///<param name="model">MessageTemplateViewModel</param>
        ///<param name="MessageTemplateEntity">DataAccess.MessageTemplate</param>
        ///<returns>MessageTemplateViewModel</returns>
        public static MessageTemplateViewModel ToViewModel(
         this DataAccess.MessageTemplate entity,
         MessageTemplateViewModel model)
        {

            model.Subject = entity.Subject;
            model.Name = entity.Name;
            model.BccEmailAddresses = entity.BccEmailAddresses;
            model.Body = entity.Body;
            model.DelayBeforeSend = entity.DelayBeforeSend;
            model.DelayHours = entity.DelayHours;
            model.EmailAccountId = entity.EmailAccountId;
            model.Name = entity.Name;
            model.FromAddress = entity.FromAddress;
            model.RoleIds = entity.ApplicationRoleMessageTemplates.Select(a => a.ApplicationRoleId);
            return model;
        }



    }
}
