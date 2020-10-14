using BusinessObject.Component;
using BusinessObject.ViewModel;
using DataAccess;
using DomainObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class EmailAccountTransform
    {

        /// <summary>
        /// Convert EmailAccount Object into EmailAccount Entity
        /// </summary>
        ///<param name="entity">IQueryable DataAccess.EmailAccount</param>
        ///<returns>IEnumerable EmailAccount</returns>
        ///
        public static IEnumerable<EmailAccountViewModel> ToListViewModel(
            this IQueryable<DataAccess.EmailAccount> entity)
        {
            return entity
                    .Include(c => c.CreatedUser)
               .Include(c => c.UpdatedUser)
                .AsNoTracking()
                .Select(a =>
                        new EmailAccountViewModel
                        {
                            Id = a.Id,
                            Email = a.Email,
                            DisplayName = a.DisplayName,
                            Host = a.Host,
                            Port = a.Port,
                            Username = a.Username,
                            Password = a.Password,
                            EnableSsl = a.EnableSsl,
                            UseDefaultCredentials = a.UseDefaultCredentials,
                            IsActive = a.IsActive,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.Email : a.CreatedUser.Email,
                        });
        }

        /// <summary>
        /// Convert EmailAccount Object into EmailAccount Entity
        /// </summary>
        ///<param name="model">EmailAccount</param>
        ///<param name="EmailAccountEntity">DataAccess.EmailAccount</param>
        ///<returns>DataAccess.EmailAccount</returns>
        public static DataAccess.EmailAccount ToEntity(this EmailAccountViewModel model,
             DataAccess.EmailAccount entity)
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
            entity.Email = model.Email;
            entity.DisplayName = model.DisplayName;
            entity.Host = model.Host;
            entity.Port = model.Port;
            entity.Username = model.Username;
            entity.Password = model.Password;
            entity.EnableSsl = model.EnableSsl;
            entity.UseDefaultCredentials = model.UseDefaultCredentials;

            return entity;
        }

        /// <summary>
        /// Convert EmailAccount Entity  into EmailAccount Object
        /// </summary>
        ///<param name="model">EmailAccountViewModel</param>
        ///<param name="EmailAccountEntity">DataAccess.EmailAccount</param>
        ///<returns>EmailAccountViewModel</returns>
        public static EmailAccountViewModel ToViewModel(
         this DataAccess.EmailAccount entity,
         EmailAccountViewModel model)
        {

            model.SessionUserId = entity.CreatedUserId;
            model.Email = entity.Email;
            model.DisplayName = entity.DisplayName;
            model.Host = entity.Host;
            model.Port = entity.Port;
            model.Username = entity.Username;
            model.Password = entity.Password;
            model.EnableSsl = entity.EnableSsl;
            model.UseDefaultCredentials = entity.UseDefaultCredentials;
            return model;
        }

    }

}
